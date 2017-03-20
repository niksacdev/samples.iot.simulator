
# **.NET Core Simulator sample for Azure IoT Hub**
The sample code is supplement to the blog post [Developing a .NET Core Simulator for Azure IoT Hub using VS for mac]
The code provides a .NET Core based simulator for connecting to IoT Hub and performing the following operations:
1. Send a message using a .NET Core simulator to Azure IoT Hub using AMQP as the protocol.
2. Receive a message using a .NET Core consumer
3. Send a command to a device and receive an acknowledgment from the device (coming soon).
The solution has been entirely developed using VS for Mac (preview) using macOS (10.12.3) but should be able to run on any .NET Core compatible IDE. The dot net standard assemblies using [v1.6] and the dot net core version is [1.1].
## Project structure
The solution contains the following projects:
1. **samples.iot.core:** Contains core components, interfaces, base entity model to be used by the sample. It also contains some shared functionality such as Configuration handlers, token providers, connectivity modules.
2.  **samples.iot.providers.sender:** Sender factory implementation for sending messages to IoT Hub.
3. **samples.iot.simulator.sender:** The .NET Core Console app that provides an interface to configure your IoT Hub instance and send messages.
4. **samples.iot.simulator.consumer:** The .NET Core Console app that provides an interface to consume incoming messages from the Event Hub Compatible Azure IoT Hub endpoint. 
5. **samples.iot.strategies.amqp:** AMQP Lite strategy for IoT Hub. The sample follows a strategy patterns for the underlying modules so for example, the current implementation being used is [AMQP Lite]("https://github.com/Azure/amqpnetlite"), if you want to switch the implementation to a different AMQP provider, simply create your strategy and inject that to the provider. 
```
	//Using AMQP lite strategy to send the message
	IDeviceSendStrategy deviceSendStrategy = new DeviceSendAMQPLiteStrategy();
	// Send the message
	await new DeviceSenderFactory().GetSender().SendMessageAsync(messageAsBytes, deviceContext, deviceSendStrategy);
```
	
6. **samples.iot.simulator.contracts:** Shared contracts between sender, receiver and consumer projects. The current sample leverages `protobuf` as the data interchange protocol and the the [`protobuf.net`] assembly is being used to provide the serialization and de-serialization. 

```
	namespace samples.iot.simulator.contract
	{
		[ProtoContract]
		public class VehicleStatus
		{
			[ProtoMember(1)]
			public Guid Id { get; set; }
	
			[ProtoMember(2)]
			public int LockStatus { get; set; }
	
			[ProtoMember(3)]
			public int TirePessure { get; set; }
	
			[ProtoMember(4)]
			public int MilesRemaining { get; set; }
	
			[ProtoMember(5)]
			public bool SportMode { get; set; }
	
			[ProtoMember(6)]
			public int IgnitionStatus { get; set; }
	
			[ProtoMember(7)]
			public int ParkStatus { get; set; }
	
			[ProtoMember(8)]
			public int AlarmStatus { get; set; }
	
			[ProtoMember(9)]
			public int AirBagStatus { get; set; }
	
		}
	}
```
	
7. **samples.iot.providers.receiver:** Includes the implementation of the receiving commands from services and responding with acknowledgement. (Coming Soon)
## How to run the sample
To run the sample, follow the steps below:
1. Clone the repo and ensure you are able to compile the projects after restoring all nugget packages.
2. The `samples.iot.simulator.sender` and `samples.iot.simulator.consumer` projects leverage a custom configuration model which leverages JSON objects. Edit the `settings.json` in the project for these projects to provide your IoT Hub and [Event Hub compatible endpoint] details respectively.
	**samples.iot.simulator.sender**
	```
	{
	   "settings":{
		  "connectionStrings":[
			 {
				"name":"your iothub",
				"connectionString":"your IoTHubHostName (e.g. myiothub.azure-devices.net)",
				"sasKey":"your device token",
				"sasKeyName":"device"
			 }
		  ],
		  "deviceId":"your device Id"
	   }
	}
```
	**samples.iot.simulator.consumer**
	```
		{
		   "settings":{
			  "connectionStrings":[
				 {
					"name":"your iothub",
					"connectionString":"Endpoint=sb://youriothubEventHubendpoint.servicebus.windows.net/",
					"sasKey":"your serviceToken",
					"sasKeyName":"service"
				 }
			  ],
			  "deviceId":"your device Id"
		   }
		}
```		
		
3. Build the solution.
4. Run the `samples.iot.simulator.sender` project, this should generate a `VehicleStatus` messages, encode it as a `protobuf` packet and then send to your IoT Hub.
5. Run the `samples.iot.simulator.consumer` project, this should scan all partitions, find the message for the Device and then decode the `protobuf` message to show current Alarm status for the device.
## Known Issues
1. The `samples.iot.simulator.consumer` project uses a custom implementation of processing messages from the Event Hub compatible endpoint. It was done because the current [Event Hub Processor nuget package] was not working properly with .NET Core. The code will be updated to use an EventProcessor host implementation once the issue is fixed.
2. The codebase is a sample and is provided AS-IS, it has not been tested for performance, fault tolerance, scalability, exception handling and instrumentation scenarios. 
