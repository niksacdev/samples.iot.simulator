using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using ProtoBuf;
using samples.iot.simulator.contract;
using samples.iot.core;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
namespace samples.iot.simulator.consumer
{
	/// <summary>
	/// Program.
	/// </summary>
	class Program
	{
		static readonly List<string> activePartitions = new List<string>();
		static readonly List<string> processedSequenceNumbers = new List<string>();

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{
			try
			{
				/// Get current message from EH compatible IoT Hub end point.
				ReceiveByConsumerGroupAsync().Wait();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"ERROR {ex.Message} {ex.StackTrace}");
			}
			finally
			{
				Console.ReadLine();
			}

		}

		/// <summary>
		/// Receives the by consumer group async.
		/// </summary>
		/// <returns>The by consumer group async.</returns>
		static async Task ReceiveByConsumerGroupAsync()
		{
			// TODO: Use Event Processor instead, currently the .netcore version for EH Processor has a compatbility issue.
			//WARNING !!!! : USE FOR SAMPLE PURPOSES ONLY
			// The below code can become a very IO / CPU intensive code considering the number of partitions you have have.
			// The ideal appraoch will be to use the EH Processor once the .netcore compatibility issue has been fixed.
			// Please use this link to know more about EventProcessorHost https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-programming-guide

			Console.WriteLine("Start consumer run for processing IoT Hub messages");
			// Get the configuration from config
			var configurationHandler = ConfigurationHandler.Instance;
			var settings = configurationHandler.GetConfiguration();

			if (settings != null && settings.ConnectionStrings.Count > 0)
			{
				var eventHubConnection = settings.ConnectionStrings[0].ConnectionString;
				var evenhubPath = settings.ConnectionStrings[0].Name;
				var sasKey = settings.ConnectionStrings[0].SasKey;
				var sasKeyName = settings.ConnectionStrings[0].SasKeyName;

				if ((null == eventHubConnection) || (null == evenhubPath) || (null == sasKey) || (null == sasKeyName))
				{
					Console.WriteLine("Invalid EH Configuration, exiting ....");
					return;
				}

				var connectionStringBuilder = new EventHubsConnectionStringBuilder(eventHubConnection)
				{
					EntityPath = evenhubPath,
					SasKey = sasKey,
					SasKeyName = sasKeyName
				};

				var mergedConnectionString = connectionStringBuilder.ToString();
				var client = EventHubClient.CreateFromConnectionString(mergedConnectionString);
				var eventHubRuntime = await client.GetRuntimeInformationAsync();
				var partitions = eventHubRuntime.PartitionIds;

				// get the active partitions
				// Process only active partitions
				var deviceId = settings.DeviceId;
				// Loop through active partitions to check for messages processed in last 5 minutes.
				// the while loop is to create a recurring check for incoming messages. 
				Parallel.ForEach(partitions, async (partition) =>
			   {
				   Console.WriteLine($"Checking for messages on partition {partition} ...");
				   // Get the partition info and skip any partitions that have not been touched in last 5 mins
				   var partitionInfo = await client.GetPartitionRuntimeInformationAsync(partition);
				   if (partitionInfo.LastEnqueuedOffset.Equals("-1"))
				   {
					   Console.WriteLine($"Offset set to -1 for Partition {partition}, no activity so skipping partition");
					   return;
				   }

				   // If last enqued time is not within 5 mins from now, skip partition
				   if (partitionInfo.LastEnqueuedTimeUtc < DateTime.UtcNow.AddMinutes(-5))
				   {
					   Console.WriteLine($"No activity in partition {partition} for last few minutes, skipping for sample processing");
					   return;
				   }

				   await ProcessMessagesinPartition(client, deviceId, partition);
			   });
			}
		}

		/// <summary>
		/// Processes the messagesin active partitions.
		/// </summary>
		/// <returns>The messagesin active partitions.</returns>
		/// <param name="client">Client.</param>
		/// <param name="deviceId">Device identifier.</param>
		static async Task ProcessMessagesinPartition(EventHubClient client, string deviceId, string partition)
		{
			// Process only active partitions

			// Get the partition info
			Console.WriteLine($" Starting to process Partition: {partition}");

			// Get all messages sent in last five minutes.
			Console.WriteLine($"Creating receiver for checking messages on partition {partition} ...");
			var receiver = client.CreateReceiver("$Default", partition, DateTime.Now.AddMinutes(-5));
			var messages = await receiver.ReceiveAsync(10); // gets 10 messages max
			if (messages == null) return;
			Console.WriteLine($"receiver returned, now checking messages on partition {partition} ...");
			foreach (var item in messages)
			{
				object sequenceNumber = null;
				foreach (var itemProp in item.Properties)
				{
					Console.WriteLine($" {itemProp.Key} - {itemProp.Value}");
					item.Properties.TryGetValue("x-opt-sequence-number", out sequenceNumber);
				}

				object subject = null;
				item.Properties.TryGetValue("iothub-connection-device-id", out subject);
				if (subject == null)
				{
					Console.WriteLine($" No Device Id property in message, skipping to next");
					return;
				}

				if (subject.ToString() != deviceId) // Set this to your device Id in settings.json
				{
					Console.WriteLine($" No match for Device Id found in Properties, skipping to next");
					return;
				}

				// if message already processed in this run, skip
				if (processedSequenceNumbers.Contains(sequenceNumber.ToString()))
				{
					Console.WriteLine($"Message with sequence number {sequenceNumber} already processed for Device Id {subject}");
					return;
				}

				// If message is still need to be proceesed deserialize and process
				processedSequenceNumbers.Add(sequenceNumber.ToString());
				var body = item.Body.Array;
				var deviceMessage = Serializer.Deserialize<VehicleStatus>(new MemoryStream((body)));
				Console.WriteLine($" Message received from Device - Message: {deviceMessage.Id} \n AlarmStatus = {deviceMessage.AlarmStatus}");
			}

			await receiver.CloseAsync();
		}
	}
}
