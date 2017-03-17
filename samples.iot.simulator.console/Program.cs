using System;
using System.Text;
using System.Threading.Tasks;
using Amqp;
using Amqp.Framing;
using samples.iot.simulator.core;
namespace samples.iot.simulator.console
{
	class Program
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{
			try
			{
				//Execute the send command to send a message to IoT Hub
				Run().Wait();
			}
			catch (Exception ex)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.WriteLine($"{ex.Message} { ex.StackTrace}");
				Console.ReadLine();
			}
		}

		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <returns>The command.</returns>
		static async Task Run()
		{
			try
			{
				// TODO: Get values from Config instead
				DeviceContext deviceContext = new DeviceContext
				{
					DeviceId = "D1234",
					DeviceKey = "X9GneHneA4Hsnrz7hFRBBpLRIHBKR5xzhLZuz6dpszo=",
					IoTHubHostName = "azdevmaciothub.azure-devices.net",
					Port = (int)DeviceProtocol.AMQPS
				};
				string message = string.Empty;

				//Using AMQP lite strategy to send the message
				IDeviceSendStrategy deviceSendStrategy = new DeviceSendAMQPLiteStrategy();

				// Send the message
				await new DeviceSenderFactory().GetSender().SendMessageAsync(message, deviceContext, deviceSendStrategy);
			}
			catch (Exception ex)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.WriteLine($"{ex.Message} { ex.StackTrace}");
				throw;
			}
		}
	}

}
