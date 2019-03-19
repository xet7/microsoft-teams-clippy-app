//----------------------------------------------------------------------------------------------
// <copyright file="IClippySetRepository.cs" company="Microsoft">
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
    /// Interface describing all the methods of a class that can retrieve <see cref="ClippySet"/> objects.
    /// </summary>
    public interface IClippySetRepository
    {
        /// <summary>
        /// Returns a <see cref="ClippySet"/> object.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>The resulting <see cref="ClippySet"/> if one was found. Null otherwise.</returns>
        Task<ClippySet> FetchClippySetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
