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
	public interface IDeviceReceiveFactory
	{
		/// <summary>
		/// Gets the receiver.
		/// </summary>
		/// <returns>The receiver.</returns>
		IDeviceReceiver GetReceiver();
	}
}
