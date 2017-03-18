// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Text;
using System.Threading.Tasks;
using Amqp;
using Amqp.Framing;
using Newtonsoft.Json;
using samples.iot.core;

namespace samples.iot.strategies.amqp
{
	/// <summary>
	/// Sender Strategy.
	/// </summary>
	public class DeviceSendAMQPLiteStrategy : IDeviceSendStrategy
	{
		/// <summary>
		/// Executes the operation async.
		/// </summary>
		/// <returns>The operation async.</returns>
		/// <param name="message">Message.</param>
		/// <param name="deviceContext">Device context.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public async Task ExecuteOperationAsync<T>(T message, DeviceContext deviceContext)
		{
			Session session = null;
			Connection connection = null;

			try
			{
				Trace.TraceLevel = Amqp.TraceLevel.Frame | Amqp.TraceLevel.Verbose;
				var deviceId = deviceContext.DeviceId;
				var iothubHostName = deviceContext.IoTHubHostName;

				// create sender (D2C) and receiver (C2D) links 
				var senderLink = $"/devices/{deviceId}/messages/events";
				var receiverLink = $"/devices/{deviceId}/messages/deviceBound"; // dont need it for now

				// Create a connection using AMQPS
				connection = await Connection.Factory.CreateAsync(new Address(iothubHostName, deviceContext.Port));
				session = new Session(connection);

				string audience = Fx.Format("{0}/devices/{1}", iothubHostName, deviceId);
				string resourceUri = Fx.Format("{0}/devices/{1}", iothubHostName, deviceId);

				// Generate the SAS token
				string sasToken = TokenGenerator.GetSharedAccessSignature(null, deviceContext.DeviceKey, resourceUri, new TimeSpan(1, 0, 0));
				bool cbs = TokenGenerator.PutCbsToken(connection, iothubHostName, sasToken, audience);
				if (cbs)
				{
					// create a session and send a telemetry message
					session = new Session(connection);
					await SendEventAsync(deviceId, message, session);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(Amqp.TraceLevel.Error, $"{ex.Message} {ex.StackTrace}");
				throw;
			}
			finally
			{
				//TODO: Need to verify how to send them as continuation token
				session.Close();
				connection.Close();
			}
		}

		/// <summary>
		/// Sends the event async.
		/// </summary>
		/// <returns>The event async.</returns>
		/// <param name="deviceId">Device identifier.</param>
		/// <param name="incomingMessage">Incoming message.</param>
		/// <param name="session">Session.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		async Task SendEventAsync<T>(string deviceId, T incomingMessage, Session session)
		{
			string entity = Fx.Format("/devices/{0}/messages/events", deviceId);
			SenderLink senderLink = new SenderLink(session, "sender-link", entity);
			try
			{
				var parsedMessage = JsonConvert.SerializeObject(incomingMessage);
				var messageValue = Encoding.UTF8.GetBytes(parsedMessage);
				Message message = new Message()
				{
					BodySection = new Data { Binary = messageValue }
				};
				await senderLink.SendAsync(message);
			}
			finally
			{
				//TODO: need to use continuation token instead.
				senderLink.Close();
			}
		}
	}
}
