using System;
using System.Threading.Tasks;
using samples.iot.core;

namespace samples.iot.strategies.amqp
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
