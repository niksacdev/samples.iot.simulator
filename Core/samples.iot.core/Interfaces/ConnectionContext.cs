// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
namespace samples.iot.core
{
	/// <summary>
	/// Connection context.
	/// </summary>
	public abstract class ConnectionContext
	{
		/// <summary>
		/// Gets or sets the name of the io TH ub host.
		/// </summary>
		/// <value>The name of the io TH ub host.</value>
		public string HostName { get; set; }

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		/// <value>The port.</value>
		public int Port { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>The protocol.</value>
        public DeviceProtocol Protocol { get; set; }
	}
}
