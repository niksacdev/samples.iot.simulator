// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
namespace samples.iot.core
{
	public enum DeviceProtocol: int
	{
		HTTPS = 443,
		AMQPS = 5672,
		AMQP = 5671,
		MQTT = 8883
	}
}
