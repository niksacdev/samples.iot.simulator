// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using samples.iot.core;

namespace samples.iot.providers.sender
{
	/// <summary>
	/// Device sender factory.
	/// </summary>
	public class DeviceSenderFactory : IDeviceSenderFactory
	{
		/// <summary>
		/// Gets the sender based on protocol.
		/// </summary>
		/// <returns>The sender.</returns>
		public IDeviceSender GetSender()
		{
			// returning default implementation, can be changed to return specialized version
			return new DeviceSender();
		}
	}
}
