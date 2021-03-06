﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Threading.Tasks;
using samples.iot.core;

namespace samples.iot.providers.receiver
{
	/// <summary>
	/// Device sender.
	/// </summary>
	public class DeviceReceiver : IDeviceReceiver
	{
		/// <summary>
		/// Receives the message async.
		/// </summary>
		/// <returns>The message async.</returns>
		/// <param name="deviceContext">Device context.</param>
		/// <param name="strategy">Strategy.</param>
		public async Task ReceiveMessageAsync(DeviceContext deviceContext, IDeviceReceiveStrategy strategy)
		{
			// execute the send command on the strategy
			await strategy.ExecuteOperationAsync(deviceContext);
		}
	}
}
