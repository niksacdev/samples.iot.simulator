// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Threading.Tasks;
namespace samples.iot.core
{
	/// <summary>
	/// Device send strategy.
	/// </summary>
	public interface IDeviceSendStrategy
	{
		/// <summary>
		/// Executes the command async.
		/// </summary>
		/// <returns>The command async.</returns>
		/// <param name="message">Message.</param>
		/// <param name="deviceContext">Device context.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task ExecuteOperationAsync<T>(T message, DeviceContext deviceContext);
	}
}
