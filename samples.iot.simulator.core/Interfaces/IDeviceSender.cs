using System;
using System.Threading.Tasks;

namespace samples.iot.core
{
	/// <summary>
	/// Device sender.
	/// </summary>
	public interface IDeviceSender
	{
		/// <summary>
		/// Sends the message async.
		/// </summary>
		/// <returns>The message async.</returns>
		/// <param name="">.</param>
		/// <param name="deviceContext">Device context.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		Task SendMessageAsync<T>(T message, DeviceContext deviceContext, IDeviceSendStrategy strategy);
	}
}
