//----------------------------------------------------------------------------------------------
// <copyright file="ClippySetRepository.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Providers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Clippy.Interfaces;
    using Clippy.Models;
    using Clippy.Providers.Serialization;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// A concrete implementation of the <see cref="IClippySetRepository"/> interface.
    /// </summary>
    public class ClippySetRepository : IClippySetRepository
    {
#pragma warning disable SA1118 // Parameter must not span multiple lines
        private static readonly ClippySet DefaultClippySet = new ClippySet(
            "Clippy!",
            new Clippy[]
            {
                new Clippy("I adore that!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Admire.gif"), new string[] { "clippy", "eyes", "admire", "wide-eyed", "wideeyed", "eyed", "sparkle", "jealous", "adore" }),
                new Clippy("Are you there?", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_AreYouThere.gif"), new string[] { "clippy", "speech", "bubble", "question", "are", "you", "there?" }),
                new Clippy("Injured", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Bandaged.gif"), new string[] { "clippy", "bandages", "bandaged", "hurt", "injured", "sick", "ill", "unwell", "bandaid", "broken" }),
                new Clippy("Bored", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Bored.gif"), new string[] { "clippy", "bored", "tired", "click", "laptop", "desk", "social", "media", "web", "distract", "distracted" }),
                new Clippy("Brb!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Brb.gif"), new string[] { "clippy", "brb!", "be", "right", "back", "speech", "bubble" }),
                new Clippy("Coffee?", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Coffee.gif"), new string[] { "clippy", "coffee?", "cup", "drink", "caffeine", "espresso", "latte", "hot", "steam", "thermos", "raised", "eyebrow" }),
                new Clippy("Coffee?", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_CoffeeMug.gif"), new string[] { "clippy", "coffee?", "mug", "cup", "drink", "hot", "steam", "caffeine" }),
                new Clippy("Happy Hour!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_HappyHour.gif"), new string[] { "clippy", "beer", "happy", "hour!", "drink", "booze", "cheers", "bar", "drunk" }),
                new Clippy("Hello there!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_HelloThere.gif"), new string[] { "clippy", "speech", "bubble", "hello", "there!", "hi" }),
                new Clippy("I'm here!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_ImHere.gif"), new string[] { "clippy", "speech", "bubble", "here!", "i'm" }),
                new Clippy("Looks great!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_LooksGreat.gif"), new string[] { "clippy", "speech", "bubble", "looks", "great!", "good", "nice" }),
                new Clippy("Love <3", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Love.gif"), new string[] { "clippy", "love", "hearts", "blush", "like", "pink", "<3" }),
                new Clippy("Meeting time!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Meeting.gif"), new string[] { "clippy", "meet", "meeting", "present", "interview", "notes", "laptop", "desk", "office", "talk", "chat", "discuss", "speech", "bubbles", "time!" }),
                new Clippy("Nap time!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Nap.gif"), new string[] { "clippy", "nap", "desk", "tired", "sleep", "snore", "rest", "time!" }),
                new Clippy("Nice!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Nice.gif"), new string[] { "clippy", "nice!", "speech", "bubble" }),
                new Clippy("No Probs!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_NoProbs.gif"), new string[] { "clippy", "no", "problem", "speech", "bubble", "probs!" }),
                new Clippy("Hi! I'm Clippy!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Normal.gif"), new string[] { "clippy!", "hi!", "hello", "wave", "introduce" }),
                new Clippy("Ok!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Ok.gif"), new string[] { "clippy", "speech", "bubble", "ok!", "okay", "yes", "agree" }),
                new Clippy("OOF", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Oof.gif"), new string[] { "clippy", "desk", "oof", "out", "facilities", "off", "gone", "office" }),
                new Clippy("Getting paid!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Payday.gif"), new string[] { "clippy", "money", "paid!", "dollars", "rain", "green", "payday", "pay", "rich", "wealthy" }),
                new Clippy("It looks like you're trying to post a giphy", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_PostGiphy.gif"), new string[] { "clippy", "help", "speech", "bubble", "giphy", "post", "ask", "question", "would", "looks", "trying" }),
                new Clippy("It looks like you meant to post a link", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_PostLink.gif"), new string[] { "clippy", "help", "link", "speech", "bubble", "post", "ask", "question", "would", "looks", "trying" }),
                new Clippy("Business, business, business. Numbers?", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Presentation.gif"), new string[] { "clippy", "present", "presentation", "meeting", "chart", "talk", "show", "graph", "business", "numbers?" }),
                new Clippy("Sad :(", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Sad.gif"), new string[] { "clippy", "sad", "blue", "blues", "disappointed", "disappoint", ":(" }),
                new Clippy("Ship it!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_ShipIt.gif"), new string[] { "clippy", "ship", "it", "captain", "complete", "done" }),
                new Clippy("Sick :(", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Sick.gif"), new string[] { "clippy", "sick", "ill", "unwell", "barf", "puke", "green", ":(" }),
                new Clippy("Spy", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Spy.gif"), new string[] { "clippy", "sneak", "peek", "spy", "creep", "suspicious" }),
                new Clippy("Poor investments", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_StocksDown.gif"), new string[] { "clippy", "poor", "stocks", "bad", "red", "broke", "graph", "chart", "down" }),
                new Clippy("Sun!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Sun.gif"), new string[] { "clippy", "sun!", "sunshine", "sweat", "sunglasses", "shades", "hot", "glasses" }),
                new Clippy("Cool!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Sunglasses.gif"), new string[] { "clippy", "cool!", "star", "sunglasses", "shades", "peek", "glasses" }),
                new Clippy("Suprise!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Surprise.gif"), new string[] { "clippy", "surprise!", "surprised", "jump", "startle", "gasp", "!!" }),
                new Clippy("Thank you!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_ThankYou.gif"), new string[] { "clippy", "speech", "bubble", "thanks", "appreciate", "you!" }),
                new Clippy("Stuck in traffic!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Traffic.gif"), new string[] { "clippy", "car", "traffic!", "rage", "road", "drive", "commute", "gridlock", "congested", "angry", "slow", "stuck", "in" }),
                new Clippy("What?", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_What.gif"), new string[] { "clippy", "question", "ask", "what?", "speech", "bubble", "?" }),
                new Clippy("It looks like you're writing a letter", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_WriteLetter.gif"), new string[] { "clippy", "help", "ask", "question", "speech", "bubble", "would", "letter", "write", "looks", "like" }),
                new Clippy("Yeah!", new Uri("https://msteamsclippy.azureedge.net/150/Clippy_Yeah.gif"), new string[] { "clippy", "yeah!", "speech", "bubble", "yay", "hooray", "yes" })
            });
#pragma warning restore SA1118 // Parameter must not span multiple lines

        private readonly ILogger logger;
        private readonly ISettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClippySetRepository"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        /// <param name="settings">The <see cref="ISettings"/>.</param>
        public ClippySetRepository(ILogger logger, ISettings settings)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <inheritdoc />
        public async Task<ClippySet> FetchClippySetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var scope = this.logger.BeginScope($"{nameof(ClippySetRepository)}.{nameof(this.FetchClippySetAsync)}"))
            {
                var configUri = this.settings.ConfigUri;
                if (configUri == null)
                {
                    this.logger.LogInformation($"{nameof(this.settings.ConfigUri)} was not a valid absolute URI; default clippy set will be used.");
                    return DefaultClippySet;
                }

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(configUri);
                    if (!response.IsSuccessStatusCode)
                    {
                        this.logger.LogError($"GET {configUri} returned {response.StatusCode}: {response.ReasonPhrase}; default clippy set will be used.");
                        return DefaultClippySet;
                    }

                    try
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var clippyConfig = JsonConvert.DeserializeObject<ClippyConfigDTO>(responseContent);
                        var clippies = clippyConfig?.Images?.Select(image => new Clippy(image.Name, new Uri(image.ImageUri), image.Keywords)).ToArray();
                        return new ClippySet("Clippy!", clippies);
                    }
                    catch (JsonException e)
                    {
                        this.logger.LogError(e, $"Response from GET {configUri} could not be parsed properly; default clippy set will be used.");
                        return DefaultClippySet;
                    }
                }
            }
        }
    }
}
