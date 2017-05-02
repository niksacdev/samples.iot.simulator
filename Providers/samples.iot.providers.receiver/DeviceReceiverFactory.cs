// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using samples.iot.core;

namespace samples.iot.providers.receiver
{
	/// <summary>
	/// Device sender factory.
	/// </summary>
	public class DeviceReceiverFactory : IDeviceReceiveFactory
	{
		/// <summary>
		/// Gets the receiver.
		/// </summary>
		/// <returns>The receiver.</returns>
		public IDeviceReceiver GetReceiver()
		{
			// returning default implementation, can be changed to return specialized version
			return new DeviceReceiver();
		}
	}
}
