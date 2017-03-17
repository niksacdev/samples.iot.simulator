using System;
using System.Threading.Tasks;

namespace samples.iot.simulator.core
{
	/// <summary>
	/// Device receiver.
	/// </summary>
	public interface IDeviceReceiver
	{
		/// <summary>
		/// Receives the message async.
		/// </summary>
		/// <returns>The message async.</returns>
		/// <param name="deviceContext">Device context.</param>
		/// <param name="strategy">Strategy.</param>
		Task ReceiveMessageAsync(DeviceContext deviceContext, IDeviceReceiveStrategy strategy);
	}
}
