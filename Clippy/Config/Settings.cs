//----------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Config
{
    using System;
    using Clippy.Interfaces;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Concrete implementation of the <see cref="ISettings"/> interface that uses the <see cref="ConfigurationBuilder"/> class to get the config.
    /// </summary>
    public class Settings : ISettings
    {
        private readonly IConfigurationRoot config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="context">The <see cref="ExecutionContext"/>.</param>
        public Settings(ExecutionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <inheritdoc/>
        public string MicrosoftAppId => this.config["MicrosoftAppId"];
    }
}
