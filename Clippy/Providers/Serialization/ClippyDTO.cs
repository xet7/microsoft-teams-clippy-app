//----------------------------------------------------------------------------------------------
// <copyright file="ClippyDTO.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Providers.Serialization
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Model describing the <see cref="ClippyDTO"/> object.
    /// </summary>
    public class ClippyDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image uri.
        /// </summary>
        [JsonProperty("imageUri")]
        public string ImageUri { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        [JsonProperty("keywords")]
        public IEnumerable<string> Keywords { get; set; }
    }
}
