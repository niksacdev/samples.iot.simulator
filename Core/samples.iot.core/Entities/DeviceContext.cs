﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
namespace samples.iot.core
{
	/// <summary>
	/// Device context.
	/// </summary>
	public sealed class DeviceContext : ConnectionContext
	{
	
		/// <summary>
		/// Gets or sets the device identifier.
		/// </summary>
		/// <value>The device identifier.</value>
		public string DeviceId { get; set; }

		/// <summary>
		/// Gets or sets the device key.
		/// </summary>
		/// <value>The device key.</value>
		public string DeviceKey { get; set; }

	}
}
