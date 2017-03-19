// // ------------------------------------------------------------
// //  Copyright (c) Microsoft Corporation.  All rights reserved.
// //  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// // ------------------------------------------------------------
//
using System;
using ProtoBuf;

namespace samples.iot.simulator.sender
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
