// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.IO;
using System.Threading.Tasks;
using ProtoBuf;
using samples.iot.core;
using samples.iot.providers.sender;
using samples.iot.simulator.contract;
using samples.iot.strategies.amqp;
using Serilog;

namespace samples.iot.simulator.sender
{
	/// <summary>
	/// Program.
	/// </summary>
	class Program
	{
        /// <summary>
        /// The logger.
        /// </summary>
        static ILogger logger;

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{
			try
			{
                // Create the SeriLog logger, using basic console logger, change / add other sinks as desired
                logger = new LoggerConfiguration()
                    .WriteTo.ColoredConsole()
                    .CreateLogger();

				//Execute the send command to send a message to IoT Hub
				SendMessageToIoTHubAsync().Wait();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{ex.Message} { ex.StackTrace}");
				Console.ReadLine();
			}
		}

		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <returns>The command.</returns>
		static async Task SendMessageToIoTHubAsync()
		{
			try
			{
				Console.WriteLine("Start sending message to IoT Hub");

				// Get the configuration from config
				var configurationHandler = ConfigurationHandler.Instance;
				var settings = configurationHandler.GetConfiguration();

				if (settings != null && settings.ConnectionStrings.Count > 0)
				{
					var connection = settings.ConnectionStrings[0].ConnectionString;
					var evenhubPath = settings.ConnectionStrings[0].Name;
					var sasKey = settings.ConnectionStrings[0].SasKey;
					var sasKeyName = settings.ConnectionStrings[0].SasKeyName;
					var deviceId = settings.DeviceId;

					DeviceContext deviceContext = new DeviceContext
					{
						DeviceId = deviceId,
						DeviceKey = sasKey,
						HostName = connection,
                        ConnectionString = connection,
						Port = (int)DeviceProtocol.AMQP // TODO: change if using a different strategy
					};

					// Build the message based on the protobuffer
                    //TODO: randomize payload
					var carStats = new VehicleStatus
					{
						AirBagStatus = 4,
						AlarmStatus = 3,
						Id = Guid.NewGuid(),
						IgnitionStatus = 0,
						LockStatus = 1,
						MilesRemaining = 40,
						ParkStatus = 1,
						SportMode = false,
						TirePessure = 35
					};

					/*
					 * Using protobuf.net to create a binary message. you may use other DIP like Thrift, Avro.
					 * The SendMessage API accepts any generic message so you can skip using a binary formatter altogether.
					*/
					byte[] messageAsBytes = default(byte[]);
					using (MemoryStream writer = new MemoryStream())
					{
						Serializer.Serialize(writer, carStats);
						messageAsBytes = writer.ToArray();
					}

					//Using AMQP lite strategy to send the message
                    IDeviceSendStrategy deviceSendStrategy = new DeviceSendIoTHubAMQPStrategy();
                   
                    // Configure the strategy
                    await deviceSendStrategy.ConfigureAsync(new DefaultCommunicationSettings { Logger = logger });

					// Send the message
					await new DeviceSenderFactory().GetSender().SendMessageAsync(messageAsBytes, deviceContext, deviceSendStrategy);
					Console.WriteLine("Message sent!");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{ex.Message} { ex.StackTrace}");
				throw;
			}
		}
	}
}

