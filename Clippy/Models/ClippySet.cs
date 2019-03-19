//----------------------------------------------------------------------------------------------
// <copyright file="ClippySet.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Model describing a set of <see cref="Clippy"/> object.
    /// </summary>
    public sealed class ClippySet : IEnumerable<Clippy>
    {
        private readonly IEnumerable<Clippy> clippies;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClippySet"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="ClippySet"/>.</param>
        /// <param name="clippies">The <see cref="Clippy"/> objects in the set.</param>
        public ClippySet(string name, IEnumerable<Clippy> clippies)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.clippies = clippies ?? throw new ArgumentNullException(nameof(clippies));
        }

        /// <summary>
        /// Gets the name of the <see cref="ClippySet"/>.
        /// </summary>
        public string Name { get; private set; }

        /// <inheritdoc />
        public IEnumerator<Clippy> GetEnumerator()
        {
            return this.clippies.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.clippies.GetEnumerator();
        }
    }
}
