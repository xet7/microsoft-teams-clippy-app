//----------------------------------------------------------------------------------------------
// <copyright file="IClippySetIndexer.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------------

namespace Clippy.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Clippy.Models;

    /// <summary>
    /// Interface describing all the methods of a class that can index a <see cref="ClippySet"/>.
    /// </summary>
    public interface IClippySetIndexer
    {
        /// <summary>
        /// Indexes a given <see cref="ClippySet"/> for future querying.
        /// </summary>
        /// <param name="clippySet">The <see cref="ClippySet"/> to index.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> to await for the result.</returns>
        Task IndexClippySetAsync(ClippySet clippySet, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Finds a non-duplicate set of <see cref="Clippy"/> objects given an input query.
        /// </summary>
        /// <param name="query">The input query.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A non-duplicate set of <see cref="Clippy"/> objects.</returns>
        Task<IEnumerable<Clippy>> FindClippiesByQuery(string query, CancellationToken cancellationToken = default(CancellationToken));
    }
}
