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
	public class ConnectionContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:samples.iot.simulator.console.ConnectionContext"/> class.
		/// </summary>
		public ConnectionContext()
		{
		}

		/// <summary>
		/// Gets or sets the name of the io TH ub host.
		/// </summary>
		/// <value>The name of the io TH ub host.</value>
		public string IoTHubHostName { get; set; }

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		/// <value>The port.</value>
		public int Port { get; set; }

	}
}
