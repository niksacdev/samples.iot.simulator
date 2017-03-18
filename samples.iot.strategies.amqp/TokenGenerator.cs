// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
using System;
using System.Text;
using Amqp;
using Amqp.Framing;
using samples.iot.core;

namespace samples.iot.strategies.amqp
{
	static class TokenGenerator
	{
		/*
		 * Adapted from ppatierno sample available at: https://github.com/ppatierno/codesamples/blob/master/IoTHubAmqp/IoTHubAmqp/Program.cs
		 */
		public static bool PutCbsToken(Connection connection, string host, string shareAccessSignature, string audience)
		{
			bool result = true;
			Session session = new Session(connection);

			string cbsReplyToAddress = "cbs-reply-to";
			var cbsSender = new SenderLink(session, "cbs-sender", "$cbs");
			var cbsReceiver = new ReceiverLink(session, cbsReplyToAddress, "$cbs");

			// construct the put-token message
			var request = new Message(shareAccessSignature);
			request.Properties = new Properties();
			request.Properties.MessageId = Guid.NewGuid().ToString();
			request.Properties.ReplyTo = cbsReplyToAddress;
			request.ApplicationProperties = new ApplicationProperties();
			request.ApplicationProperties["operation"] = "put-token";
			request.ApplicationProperties["type"] = "azure-devices.net:sastoken";
			request.ApplicationProperties["name"] = audience;
			cbsSender.Send(request);

			// receive the response
			var response = cbsReceiver.Receive();
			if (response == null || response.Properties == null || response.ApplicationProperties == null)
			{
				result = false;
			}
			else
			{
				int statusCode = (int)response.ApplicationProperties["status-code"];
				string statusCodeDescription = (string)response.ApplicationProperties["status-description"];
				if (statusCode != 202 && statusCode != 200) // !Accepted && !OK
				{
					result = false;
				}
			}

			// the sender/receiver may be kept open for refreshing tokens
			cbsSender.Close();
			cbsReceiver.Close();
			session.Close();

			return result;
		}


		internal static readonly long UtcReference = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks;

		/*
		 * Adapted from ppatierno sample available at: https://github.com/ppatierno/codesamples/blob/master/IoTHubAmqp/IoTHubAmqp/Program.cs
		 */
		public static string GetSharedAccessSignature(string keyName, string sharedAccessKey, string resource, TimeSpan tokenTimeToLive)
		{
			// http://msdn.microsoft.com/en-us/library/azure/dn170477.aspx
			// the canonical Uri scheme is http because the token is not amqp specific
			// signature is computed from joined encoded request Uri string and expiry string

			string expiry = ((long)(DateTime.UtcNow - new DateTime(UtcReference, DateTimeKind.Utc) + tokenTimeToLive).TotalSeconds).ToString();
			string encodedUri = HttpUtility.UrlEncode(resource);

			byte[] hmac = SHA.computeHMAC_SHA256(Convert.FromBase64String(sharedAccessKey), Encoding.UTF8.GetBytes(encodedUri + "\n" + expiry));
			string sig = Convert.ToBase64String(hmac);

			if (keyName != null)
			{
				return Fx.Format(
				"SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}",
				encodedUri,
				HttpUtility.UrlEncode(sig),
				HttpUtility.UrlEncode(expiry),
				HttpUtility.UrlEncode(keyName));
			}
			else
			{
				return Fx.Format(
					"SharedAccessSignature sr={0}&sig={1}&se={2}",
					encodedUri,
					HttpUtility.UrlEncode(sig),
					HttpUtility.UrlEncode(expiry));
			}
		}
	}
}
