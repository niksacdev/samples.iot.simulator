using System;
using System.Threading.Tasks;

namespace samples.iot.simulator.core
{
	/// <summary>
	/// Device receiver amqp.
	/// </summary>
	public class DeviceReceiverDefaultStrategy : IDeviceReceiveStrategy
	{
		/// <summary>
		/// Executes the operation async.
		/// </summary>
		/// <returns>The operation async.</returns>
		/// <param name="deviceContext">Device context.</param>
		public Task ExecuteOperationAsync(DeviceContext deviceContext)
		{
			throw new NotImplementedException();
		}
	}
}
