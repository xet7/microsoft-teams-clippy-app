//----------------------------------------------------------------------------------------------
// <copyright file="ClippyPreviewCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Cards
{
    using System;
    using System.Collections.Generic;
    using Clippy.Models;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// Card for the preview of the <see cref="Clippy"/> to return in the results for the Compose Extension query.
    /// </summary>
    public class ClippyPreviewCard
    {
        private readonly Clippy clippy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClippyPreviewCard"/> class.
        /// </summary>
        /// <param name="clippy">The <see cref="Clippy"/> for this card.</param>
        public ClippyPreviewCard(Clippy clippy)
        {
            this.clippy = clippy ?? throw new ArgumentNullException(nameof(clippy));
        }

        /// <summary>
        /// Turns the card into an <see cref="Attachment"/>.
        /// </summary>
        /// <returns>An <see cref="Attachment"/>.</returns>
        public Attachment ToAttachment()
        {
            var card = new ThumbnailCard
            {
                Title = this.clippy.Name,
                Images = new List<CardImage>()
                {
                    new CardImage
                    {
                        Alt = this.clippy.Name,
                        Url = this.clippy.ImageUri.ToString()
                    }
                }
            };

            return card.ToAttachment();
        }
    }
}
