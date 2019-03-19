//----------------------------------------------------------------------------------------------
// <copyright file="ClippyComposeExtensionCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Cards
{
    using System;
    using Clippy.Extensions;
    using Clippy.Models;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// Card to <see cref="Clippy"/> return in the results for the Compose Extension query.
    /// </summary>
    public class ClippyComposeExtensionCard
    {
        private readonly Clippy clippy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClippyComposeExtensionCard"/> class.
        /// </summary>
        /// <param name="clippy">The <see cref="Clippy"/> for this card.</param>
        public ClippyComposeExtensionCard(Clippy clippy)
        {
            this.clippy = clippy ?? throw new ArgumentNullException(nameof(clippy));
        }

        /// <summary>
        /// Turns the card into an <see cref="Attachment"/>.
        /// </summary>
        /// <returns>An <see cref="Attachment"/>.</returns>
        public Attachment ToAttachment()
        {
            var content = new ClippyContentCard(this.clippy);
            var preview = new ClippyPreviewCard(this.clippy);

            return content.ToAttachment().ToComposeExtensionResult(preview.ToAttachment());
        }
    }
}
