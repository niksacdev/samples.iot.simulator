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
using samples.iot.strategies.amqp;

namespace samples.iot.simulator.sender
{
	/// <summary>
	/// Program.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{
			try
			{
				//Execute the send command to send a message to IoT Hub
				SendMessageToIoTHub().Wait();
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
		static async Task SendMessageToIoTHub()
		{
			try
			{
				// TODO: Get values from Config instead
				DeviceContext deviceContext = new DeviceContext
				{
					DeviceId = "D1234",
					DeviceKey = "X9GneHneA4Hsnrz7hFRBBpLRIHBKR5xzhLZuz6dpszo=",
					IoTHubHostName = "azdevmaciothub.azure-devices.net",
					Port = (int)DeviceProtocol.AMQP
				};

				// Build the message based on the protobuffer
				var carStats = new VehicleStatus
				{
					AirBagStatus = 4,
					AlarmStatus = 1,
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
				IDeviceSendStrategy deviceSendStrategy = new DeviceSendAMQPLiteStrategy();

				// Send the message
				await new DeviceSenderFactory().GetSender().SendMessageAsync(messageAsBytes, deviceContext, deviceSendStrategy);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{ex.Message} { ex.StackTrace}");
				throw;
			}
		}
	}

}
