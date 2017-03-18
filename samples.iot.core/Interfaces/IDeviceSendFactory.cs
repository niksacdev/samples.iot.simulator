// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
namespace samples.iot.core
{
	/// <summary>
	/// Device sender factory.
	/// </summary>
	public interface IDeviceSenderFactory
	{
		/// <summary>
		/// Gets the device sender based on protocol.
		/// </summary>
		/// <returns>The sender.</returns>
		IDeviceSender GetSender();
	}
}
