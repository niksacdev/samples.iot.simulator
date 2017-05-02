// // ------------------------------------------------------------
// //  Copyright (c) Microsoft Corporation.  All rights reserved.
// //  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// // ------------------------------------------------------------
//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
namespace samples.iot.core
{
	/// <summary>
	/// Settings.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class Settings
	{
		/// <summary>
		/// Gets or sets the connection strings.
		/// </summary>
		/// <value>The connection strings.</value>
		[JsonProperty("connectionStrings", Required = Required.AllowNull)]
		public List<ConnectionStringItem> ConnectionStrings { get; set; }

		[JsonProperty("deviceId", Required = Required.AllowNull)]
		public string DeviceId { get; set; }

	}
}
