﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using samples.iot.core;

namespace samples.iot.providers.sender
{
	/// <summary>
	/// Device sender.
	/// </summary>
	public class DeviceSender : IDeviceSender
	{
		/// <summary>
		/// Sends the message async.
		/// </summary>
		/// <returns>The message async.</returns>
		/// <param name="message">Message.</param>
		/// <param name="deviceContext">Device context.</param>
		/// <param name="deviceStrategy">Device strategy.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public async Task SendMessageAsync<T>(T message, DeviceContext deviceContext, IDeviceSendStrategy deviceStrategy)
		{
			// execute the send command on the strategy
			await deviceStrategy.ExecuteOperationAsync<T>(message, deviceContext);
		}
	}
}
