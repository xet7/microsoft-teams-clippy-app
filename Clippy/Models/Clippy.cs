//----------------------------------------------------------------------------------------------
// <copyright file="Clippy.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model describing the <see cref="Clippy"/> object.
    /// </summary>
    public sealed class Clippy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clippy"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="Clippy"/>.</param>
        /// <param name="imageUri">The image <see cref="Uri"/> of the <see cref="Clippy"/>.</param>
        /// <param name="keywords">The keywords associated with the <see cref="Clippy"/>.</param>
        public Clippy(string name, Uri imageUri, IEnumerable<string> keywords)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.ImageUri = imageUri ?? throw new ArgumentNullException(nameof(imageUri));
            this.Keywords = keywords ?? throw new ArgumentNullException(nameof(keywords));
        }

        /// <summary>
        /// Gets the name of the <see cref="Clippy"/>.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the image <see cref="Uri"/> of the <see cref="Clippy"/>.
        /// </summary>
        public Uri ImageUri { get; private set; }

        /// <summary>
        /// Gets the keywords of the <see cref="Clippy"/>.
        /// </summary>
        public IEnumerable<string> Keywords { get; private set; }
    }
}
