//----------------------------------------------------------------------------------------------
// <copyright file="MessagesHttpFunction.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Clippy.Cards;
    using Clippy.Config;
    using Clippy.Extensions;
    using Clippy.Interfaces;
    using Clippy.Providers;
    using Clippy.WebModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Azure Function that handles HTTP messages from BotFramework.
    /// </summary>
    public static class MessagesHttpFunction
    {
        /// <summary>
        /// Method that is called by the Azure Function framework when this Azure Function is invoked.
        /// </summary>
        /// <param name="req">The <see cref="HttpRequest"/>.</param>
        /// <param name="log">The <see cref="ILogger"/>.</param>
        /// <param name="context">The <see cref="ExecutionContext"/>.</param>
        /// <returns>A <see cref="Task"/> that results in an <see cref="IActionResult"/> when awaited.</returns>
        [FunctionName("messages")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation("Messages function received a request.");

            ISettings settings = new Settings(context);
            IClippySetRepository clippySetRepository = new ClippySetRepository();
            IClippySetIndexer clippySetIndexer = new ClippySetIndexer();
            ICredentialProvider credentialProvider = new SimpleCredentialProvider(settings.MicrosoftAppId, null);
            IChannelProvider channelProvider = new SimpleChannelProvider();

            Activity activity;
            try
            {
                var authorizationHeader = GetAuthorizationHeader(req);
                activity = await ParseRequestBody(req);
                await JwtTokenValidation.AuthenticateRequest(activity, authorizationHeader, credentialProvider, channelProvider);
            }
            catch (JsonReaderException e)
            {
                log.LogDebug("Request payload was incorrect. JSON parser failed with '{0}'.", e.Message);
                return new BadRequestResult();
            }
            catch (UnauthorizedAccessException)
            {
                log.LogDebug("Request was not propertly authorized.");
                return new UnauthorizedResult();
            }

            if (!activity.IsComposeExtensionQuery())
            {
                log.LogDebug("Request payload was not a compose extension query.");
                return new BadRequestObjectResult($"Clippy only supports compose extension query activity types.");
            }

            var queryValue = JObject.FromObject(activity.Value).ToObject<ComposeExtensionValue>();
            var query = queryValue.GetParameterValue();

            var clippySet = await clippySetRepository.FetchClippySetAsync(Guid.Empty);
            await clippySetIndexer.IndexClippySetAsync(clippySet);
            var clippies = await clippySetIndexer.FindClippiesByQuery(query);

            var result = new ComposeExtensionResponse
            {
                ComposeExtensionResult = new ComposeExtensionResult
                {
                    Type = "result",
                    AttachmentLayout = "grid",
                    Attachments = clippies.Select(clippy => new ClippyComposeExtensionCard(clippy).ToAttachment()).ToArray()
                }
            };

            return new OkObjectResult(result);
        }

        /// <summary>
        /// Gets the authorization header from the <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="req">The <see cref="HttpRequest"/>/</param>
        /// <exception cref="UnauthorizedAccessException">If there is no authorization header.</exception>
        /// <returns>The authorization header.</returns>
        private static string GetAuthorizationHeader(HttpRequest req)
        {
            if (!req.Headers.TryGetValue("Authorization", out var authHeaders) && authHeaders.Count < 1)
            {
                throw new UnauthorizedAccessException();
            }

            return authHeaders[0];
        }

        /// <summary>
        /// Attempts to parse the <see cref="HttpRequest"/> body into a <see cref="Activity"/> object.
        /// </summary>
        /// <param name="req">The <see cref="HttpRequest"/>.</param>
        /// <exception cref="JsonReaderException">If the <see cref="HttpRequest"/> body could not be parsed properly.</exception>
        /// <returns>The <see cref="Activity"/> object.</returns>
        private static async Task<Activity> ParseRequestBody(HttpRequest req)
        {
            using (var streamReader = new StreamReader(req.Body))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var activityJObject = await JObject.LoadAsync(jsonReader);
                return activityJObject.ToObject<Activity>();
            }
        }
    }
}
