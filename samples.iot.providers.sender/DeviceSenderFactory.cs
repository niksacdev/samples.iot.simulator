using System;
using samples.iot.core;

namespace samples.iot.providers.sender
{
	/// <summary>
	/// Device sender factory.
	/// </summary>
	public class DeviceSenderFactory : IDeviceSenderFactory
	{
		/// <summary>
		/// Gets the sender based on protocol.
		/// </summary>
		/// <returns>The sender.</returns>
		public IDeviceSender GetSender()
		{
			// returning default implementation, can be changed to return specialized version
			return new DeviceSender();
		}
	}
}
