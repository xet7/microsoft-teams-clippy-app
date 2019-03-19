//----------------------------------------------------------------------------------------------
// <copyright file="ClippyConfigDTO.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Providers.Serialization
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Model describing the <see cref="ClippyConfigDTO"/> object.
    /// </summary>
    public class ClippyConfigDTO
    {
        /// <summary>
        /// Gets or sets the images in this app.
        /// </summary>
        [JsonProperty("images")]
        public ClippyDTO[] Images { get; set; }
    }
}
