using System;
namespace samples.iot.simulator.core
{
	/// <summary>
	/// Device sender factory.
	/// </summary>
	public interface IDeviceReceiveFactory
	{
		/// <summary>
		/// Gets the receiver.
		/// </summary>
		/// <returns>The receiver.</returns>
		IDeviceReceiver GetReceiver();
	}
}
