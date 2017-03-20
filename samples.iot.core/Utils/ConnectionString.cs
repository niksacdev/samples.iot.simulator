// // ------------------------------------------------------------
// //  Copyright (c) Microsoft Corporation.  All rights reserved.
// //  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// // ------------------------------------------------------------
//
using System;
using Newtonsoft.Json;

namespace samples.iot.core
{
	/// <summary>
	/// Connection string item.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class ConnectionStringItem
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[JsonProperty("name", Required = Required.Always)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		/// <value>The connection string.</value>
		[JsonProperty("connectionString", Required = Required.Always)]
		public string ConnectionString { get; set; }

		[JsonProperty("sasKey", Required = Required.Always)]
		public string SasKey { get; set; }


		[JsonProperty("sasKeyName", Required = Required.Always)]
		public string SasKeyName { get; set; }


	}
}
