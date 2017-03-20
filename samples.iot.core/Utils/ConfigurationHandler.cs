// // ------------------------------------------------------------
// //  Copyright (c) Microsoft Corporation.  All rights reserved.
// //  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// // ------------------------------------------------------------
//
using System;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace samples.iot.core
{
	/// <summary>
	/// Configuration handler.
	/// </summary>
	public class ConfigurationHandler
	{
		//TODO: use Extensions.Configuration providers instead.

		private static readonly Lazy<ConfigurationHandler> settings = new Lazy<ConfigurationHandler>(() => new ConfigurationHandler());

		readonly string configurationFileName = "settings.json";
			
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static ConfigurationHandler Instance { get { return settings.Value; } }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:samples.iot.core.ConfigurationHandler"/> class.
		/// </summary>
		private ConfigurationHandler()
		{
		}

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <returns>The configuration.</returns>
		/// <param name="renew">If set to <c>true</c> renew.</param>
		public Settings GetConfiguration(bool renew = true)
		{
			//TODO: leverage renew to only get from stream when renew is true, for now renew is always true.
			return DeserializeFromJSON<SettingsResults>(string.Empty, configurationFileName).Settings;
		}

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <returns>The configuration.</returns>
		/// <param name="path">Path.</param>
		/// <param name="renew">If set to <c>true</c> renew.</param>
		public Settings GetConfiguration(string path, bool renew = true)
		{ 
			//TODO: leverage renew to only get from stream when renew is true, for now renew is always true.
			return DeserializeFromJSON<SettingsResults>(path, configurationFileName).Settings;
		}

		/// <summary>
		/// Deserializes from yaml.
		/// </summary>
		/// <returns>The from yaml.</returns>
		/// <param name="path">Path.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public  T DeserializeFromYaml<T>(string path, string fileName)
		{
			var result = default(T);
			if (string.IsNullOrWhiteSpace(path))
			{
				// set path to current directory
				path = $"{PlatformServices.Default.Application.ApplicationBasePath}/{fileName}".Trim();
			}

			// read the yaml file
			//TODO: move to in memory cache so we can preserve till the lifetime of assembly and only refresh when renew == true
			var input = File.ReadAllText(path);

			//Deserialize to convert to desired object
			var deserializer = new DeserializerBuilder()
				.WithNamingConvention(new CamelCaseNamingConvention())
				.Build();
			return deserializer.Deserialize<T>(input);
		}


		/// <summary>
		/// Deserializes from json.
		/// </summary>
		/// <returns>The from json.</returns>
		/// <param name="path">Path.</param>
		/// <param name="fileName">File name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T DeserializeFromJSON<T>(string path, string fileName)
		{ 
			var result = default(T);
			if (string.IsNullOrWhiteSpace(path))
			{
				// set path to current directory
				path = $"{PlatformServices.Default.Application.ApplicationBasePath}/{fileName}".Trim();
			}

			// read the yaml file
			//TODO: move to in memory cache so we can preserve till the lifetime of assembly and only refresh when renew == true
			var input = File.ReadAllText(path);
			using (var jsonReader = new JsonTextReader(new StringReader(input)))
			{
				var jsonSerializerManager = new JsonSerializer();
				result = jsonSerializerManager.Deserialize<T>(jsonReader);
			}

			return result;
		}
	}
}
