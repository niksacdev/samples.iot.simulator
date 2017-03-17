using System;
using System.Threading.Tasks;

namespace samples.iot.simulator.core
{
	/// <summary>
	/// Device sender.
	/// </summary>
	public class DeviceReceiver : IDeviceReceiver
	{
		/// <summary>
		/// Receives the message async.
		/// </summary>
		/// <returns>The message async.</returns>
		/// <param name="deviceContext">Device context.</param>
		/// <param name="strategy">Strategy.</param>
		public async Task ReceiveMessageAsync(DeviceContext deviceContext, IDeviceReceiveStrategy strategy)
		{
			// execute the send command on the strategy
			await strategy.ExecuteOperationAsync(deviceContext);
		}
	}
}
