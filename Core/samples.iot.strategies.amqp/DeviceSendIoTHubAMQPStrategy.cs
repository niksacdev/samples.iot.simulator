
// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using samples.iot.core;
using System.Runtime.Serialization.Formatters;
using System.IO;
using Microsoft.Azure.Devices.Client;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace samples.iot.strategies.amqp
{
	/// <summary>
	/// Sender Strategy.
	/// </summary>
	public class DeviceSendIoTHubAMQPStrategy : IDeviceSendStrategy
	{
        ICommunicationSettings communicationSettings;
        ILogger logger;

        /// <summary>
        /// Configures the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="settings">Settings.</param>
        public Task ConfigureAsync(ICommunicationSettings settings)
        {
			communicationSettings = settings;
            logger = settings.Logger;
			return Task.CompletedTask;
        }

        /// <summary>
        /// Executes the operation async.
        /// </summary>
        /// <returns>The operation async.</returns>
        /// <param name="message">Message.</param>
        /// <param name="deviceContext">Device context.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task ExecuteOperationAsync<T>(T message, DeviceContext deviceContext)
		{
            try
			{
                if(deviceContext == null) {
                    logger.Error("deviceContext is null, cannot continue further");
                    throw new ArgumentNullException();
                }

				var deviceId = deviceContext.DeviceId;
				var iothubHostName = deviceContext.HostName;
                var connection = deviceContext.ConnectionString;

                logger.Information($"executing opertion for {deviceId} in host: {iothubHostName}");

                // Create a connection using device context for AMQP session
                DeviceClient client = DeviceClient.CreateFromConnectionString(connection,deviceId,TransportType.Amqp);

                // Build the messages for IoT hub
                var iotmessage = new Message(message as byte[]);

                // Send messages to IoT Hub
                await client.SendEventAsync(iotmessage);
			}
            catch (Exception ex)
			{
                //TODO: change to a different Serilog implemenation that can write the exception object.
                logger.Error($"{ex.Message} + {ex.StackTrace}");
				throw;
			}
		}
	}
}
