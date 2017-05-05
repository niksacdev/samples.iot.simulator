// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Threading.Tasks;
using samples.iot.core;

namespace samples.iot.strategies.amqp
{
	/// <summary>
	/// Device receiver amqp.
	/// </summary>
	public class DeviceReceiverDefaultStrategy : IDeviceReceiveStrategy
	{
        ICommunicationSettings communicationSetttings;

        /// <summary>
        /// Configures the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="settings">Settings.</param>
        public Task ConfigureAsync(ICommunicationSettings settings)
        {
            communicationSetttings = settings;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Executes the operation async.
        /// </summary>
        /// <returns>The operation async.</returns>
        /// <param name="deviceContext">Device context.</param>
        public Task ExecuteOperationAsync(DeviceContext deviceContext)
		{
			throw new NotImplementedException();
		}
	}
}
