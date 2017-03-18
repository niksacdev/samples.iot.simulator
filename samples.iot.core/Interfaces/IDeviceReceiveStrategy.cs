// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Threading.Tasks;
namespace samples.iot.core
{
	/// <summary>
	/// Device receive strategy.
	/// </summary>
	public interface IDeviceReceiveStrategy
	{
		/// <summary>
		/// Executes the command async.
		/// </summary>
		/// <returns>The command async.</returns>
		/// <param name="deviceContext">Device context.</param>
		Task ExecuteOperationAsync(DeviceContext deviceContext);
	}
}
