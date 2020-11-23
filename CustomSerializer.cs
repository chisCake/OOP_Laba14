using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml.Serialization;

namespace OOP_Laba14 {
	static class CustomSerializer {
		public static void Serialize(object item, Format format, string file) {
			var fileStream = new FileStream(file, FileMode.Create);
			using var sw = new StreamWriter(fileStream);
			switch (format) {
				case Format.Binary:
					new BinaryFormatter().Serialize(fileStream, item);
					break;
				case Format.SOAP:
					new SoapFormatter().Serialize(fileStream, item);
					break;
				case Format.JSON:
					sw.Write(JsonSerializer.Serialize(item, item.GetType()));
					break;
				case Format.XML:
					new XmlSerializer(item.GetType()).Serialize(sw, item);
					break;
				default:
					throw new NotImplementedException();
			}
		}

		public static T Deserialize<T>(Format format, string file) {
			var fileStream = new FileStream(file, FileMode.Open);
			using var sr = new StreamReader(fileStream);
			return format switch {
				Format.Binary =>
					(T)new BinaryFormatter().Deserialize(fileStream),
				Format.SOAP =>
					(T)new SoapFormatter().Deserialize(fileStream),
				Format.JSON =>
				   JsonSerializer.Deserialize<T>(sr.ReadToEnd()),
				Format.XML =>
					(T)new XmlSerializer(typeof(T)).Deserialize(fileStream),
				_ => throw new NotImplementedException()
			};
		}
	}

	enum Format {
		Binary,
		SOAP,
		JSON,
		XML
	}
}
