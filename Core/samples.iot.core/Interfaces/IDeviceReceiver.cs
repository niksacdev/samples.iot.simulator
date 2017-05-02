// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Threading.Tasks;

namespace samples.iot.core
{
	/// <summary>
	/// Device receiver.
	/// </summary>
	public interface IDeviceReceiver
	{
		/// <summary>
		/// Receives the message async.
		/// </summary>
		/// <returns>The message async.</returns>
		/// <param name="deviceContext">Device context.</param>
		/// <param name="strategy">Strategy.</param>
		Task ReceiveMessageAsync(DeviceContext deviceContext, IDeviceReceiveStrategy strategy);
	}
}
