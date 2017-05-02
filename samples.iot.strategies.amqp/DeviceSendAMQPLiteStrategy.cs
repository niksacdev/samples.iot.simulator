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
using System.Runtime.Serialization.Formatters;
using System.IO;

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
			try
			{
				Trace.TraceLevel = Amqp.TraceLevel.Frame | Amqp.TraceLevel.Verbose;
				var deviceId = deviceContext.DeviceId;
				var iothubHostName = deviceContext.IoTHubHostName;

				// Create a connection using device context
				Connection connection  = await Connection.Factory.CreateAsync(new Address(iothubHostName, deviceContext.Port));
				Session session  = new Session(connection);

				string audience = Fx.Format("{0}/devices/{1}", iothubHostName, deviceId);
				string resourceUri = Fx.Format("{0}/devices/{1}", iothubHostName, deviceId); 

				// Generate the SAS token
				string sasToken = TokenGenerator.GetSharedAccessSignature(null, deviceContext.DeviceKey, resourceUri, new TimeSpan(1, 0, 0));

				//TODO: Need to make this call asynchronous 
				bool cbs = TokenGenerator.PutCbsToken(connection, iothubHostName, sasToken, audience);
				if (cbs)
				{
					// create a session and send a telemetry message
					session = new Session(connection);
					byte[] messageAsBytes = default(byte[]);
					if (typeof(T) == typeof(byte[]))
					{
						messageAsBytes = message as byte[];
					}
					else
					{
						// convert object to byte[]
						//TODO: will need a binary formatter, not available in .net core yet.
					}

					// Get byte[] from 
					await SendEventAsync(deviceId, messageAsBytes, session);
					await session.CloseAsync();
					await connection.CloseAsync();
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(Amqp.TraceLevel.Error, $"{ex.Message} {ex.StackTrace}");
				throw;
			}
		}

		/// <summary>
		/// Sends the event async.
		/// </summary>
		/// <returns>The event async.</returns>
		/// <param name="deviceId">Device identifier.</param>
		/// <param name="incomingMessage">Incoming message.</param>
		/// <param name="session">Session.</param>
		async Task SendEventAsync(string deviceId, byte[] incomingMessage, Session session)
		{
			string entity = Fx.Format("/devices/{0}/messages/events", deviceId);
			SenderLink senderLink = new SenderLink(session, "sender-link", entity);
			Message message = new Message
			{
				// sending a property to refine messages on the egress site
				Properties = new Properties { Subject = deviceId },
				BodySection = new Data { Binary = incomingMessage }
			};

			await senderLink.SendAsync(message);
			await senderLink.CloseAsync();
		}
	}
}
