// // ------------------------------------------------------------
// //  Copyright (c) Microsoft Corporation.  All rights reserved.
// //  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// // ------------------------------------------------------------
//
using System;
using System.Threading.Tasks;
namespace samples.iot.core
{
    /// <summary>
    /// Communication strategy.
    /// </summary>
    public interface ICommunicationStrategy
    {
        /// <summary>
        /// Configures the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="settings">Settings.</param>
        Task ConfigureAsync(ICommunicationSettings settings);
    }
}
