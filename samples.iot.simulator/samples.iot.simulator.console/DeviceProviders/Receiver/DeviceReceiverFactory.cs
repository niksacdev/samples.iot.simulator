using System;
namespace samples.iot.simulator.core
{
	/// <summary>
	/// Device sender factory.
	/// </summary>
	public class DeviceReceiverFactory : IDeviceReceiveFactory
	{
		/// <summary>
		/// Gets the receiver.
		/// </summary>
		/// <returns>The receiver.</returns>
		public IDeviceReceiver GetReceiver()
		{
			// returning default implementation, can be changed to return specialized version
			return new DeviceReceiver();
		}
	}
}
