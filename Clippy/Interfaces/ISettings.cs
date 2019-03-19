//----------------------------------------------------------------------------------------------
// <copyright file="ISettings.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Clippy.Models;

    /// <summary>
    /// Interface describing all of the app settings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Gets the Microsoft App Id.
        /// </summary>
        string MicrosoftAppId { get; }
    }
}
