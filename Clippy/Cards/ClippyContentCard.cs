//----------------------------------------------------------------------------------------------
// <copyright file="ClippyContentCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Cards
{
    using System;
    using System.Collections.Generic;
    using AdaptiveCards;
    using Clippy.Extensions;
    using Clippy.Models;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// Card for the content of the <see cref="Clippy"/> to return in the results for the Compose Extension query.
    /// </summary>
    public class ClippyContentCard
    {
        private readonly Clippy clippy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClippyContentCard"/> class.
        /// </summary>
        /// <param name="clippy">The <see cref="Clippy"/> for this card.</param>
        public ClippyContentCard(Clippy clippy)
        {
            this.clippy = clippy ?? throw new ArgumentNullException(nameof(clippy));
        }

        /// <summary>
        /// Turns the card into an <see cref="Attachment"/>.
        /// </summary>
        /// <returns>An <see cref="Attachment"/>.</returns>
        public Attachment ToAttachment()
        {
            var card = new AdaptiveCard
            {
                Speak = this.clippy.Name,
                Body = new List<AdaptiveElement>()
                {
                    new AdaptiveImage
                    {
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center,
                        Url = this.clippy.ImageUri,
                        AltText = this.clippy.Name
                    }
                }
            };

            return card.ToAttachment();
        }
    }
}
