// // ------------------------------------------------------------
// //  Copyright (c) Microsoft Corporation.  All rights reserved.
// //  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// // ------------------------------------------------------------
//
using System;
using Serilog;

namespace samples.iot.core
{
    /// <summary>
    /// Default communication settings.
    /// </summary>
    public class DefaultCommunicationSettings : ICommunicationSettings
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }
    }
}
