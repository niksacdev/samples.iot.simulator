using System;
namespace samples.iot.simulator.core
{
	/// <summary>
	/// Device sender factory.
	/// </summary>
	public interface IDeviceSenderFactory
	{
		/// <summary>
		/// Gets the device sender based on protocol.
		/// </summary>
		/// <returns>The sender.</returns>
		IDeviceSender GetSender();
	}
}
