using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OOP_Laba5;

namespace OOP_Laba14 {
	class Program {
		static readonly List<Action> tasks = new List<Action>() {
			new Action(Task1),
			new Action(Task2),
			new Action(Task3),
			new Action(Task4),
		};

		static List<Product> products = new List<Product> {
			new PrintingDevice("Epson", "L132", "Чёрный", 392, 2.7, (482, 130, 222),10, PrintingTechnology.Inkjet, (5760, 1440), 27),
			new PrintingDevice("Canon", "Pixma TS304", "Чёрный", 140, 2.9, (430, 143, 282), 10, PrintingTechnology.Inkjet, (4800, 1200), 7.7),
			new Scanner("Epson", "Perfection V19", "Чёрный", 243, 1.54, (248, 364, 390), 2.5, (4800, 4800), 48),
			new Scanner("Canon", "CanoScan LiDE 400", "Чёрный", 248.15, 1.7, (250, 367, 420), 4.5, (4800, 4800), 48),
			new Computer("Jet", "Multimedia GE20D8SD24VGALW50", "Чёрный", 649, 0, (0,0,0), 500, "Windows", "AMD Athlon 200GE", "AMD Radeon Vega 3", 8, 240),
			new Computer("Z-Tech", "J190-4-10-miniPC-D-0001n", "Чёрный", 529, 0, (0,0,0), 500, "Windows", "Intel Celeron J1900", "Intel HD Graphics", 4, 1000),
			new Tablet("Lenovo", "Tab E7 TB-7104I", "Чёрный", 189, 0.272, (110, 193, 10), 0, "Android 8.0", "MediaTek MT8321A/D", "ARM Mali-400 MP", 1, 16, 7, (1024, 600), 2750),
			new Tablet("Lenovo", "M10 Plus TB-X606F", "Чёрный", 599, 0.460, (244, 153, 8), 0, "Android 9.0", "MediaTek Helio P22T", "IMG GE8320", 4, 64, 10.3, (1920, 1200), 5000)
		};

		static void Main() {
			while (true) {
				Console.Write(
					"1 - сериализация/десериализация объекта" +
					"\n2 - сериализацию/десериализацию коллекции" +
					"\n3 - селекторы для XML" +
					"\n4 - Linq to JSON" +
					"\n0 - выход" +
					"\nВыберите действие: "
					);
				if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > tasks.Count) {
					Console.WriteLine("Нет такого действия");
					Console.ReadKey();
					Console.Clear();
					continue;
				}
				if (choice == 0) {
					Console.WriteLine("Выход...");
					Environment.Exit(0);
				}
				tasks[choice - 1]();
				Console.ReadKey();
				Console.Clear();
			}
		}

		static void Task1() {
			if (Directory.Exists("task1"))
				Directory.Delete("task1", true);
			Directory.CreateDirectory("task1");

			CustomSerializer.Serialize(products[6], Format.Binary, "task1/binary.bin");
			CustomSerializer.Serialize(products[6], Format.SOAP, "task1/soap.soap");
			CustomSerializer.Serialize(products[6], Format.JSON, "task1/json.json");
			CustomSerializer.Serialize(products[6], Format.XML, "task1/xml.xml");
			Console.WriteLine("Данные сериализованы и записаны");
			Process.Start("notepad", "task1\\binary.bin");
			Process.Start("notepad", "task1\\soap.soap");
			Process.Start("notepad", "task1\\json.json");
			Process.Start("notepad", "task1\\xml.xml");

			Console.WriteLine("Десериализация:");
			var res = CustomSerializer.Deserialize<Tablet>(Format.Binary, "task1/binary.bin");
			Console.WriteLine("\nBinary");
			Print(res);
			res = CustomSerializer.Deserialize<Tablet>(Format.SOAP, "task1/soap.soap");
			Console.WriteLine("\nSoap");
			Print(res);
			res = CustomSerializer.Deserialize<Tablet>(Format.JSON, "task1/json.json");
			Console.WriteLine("\nJson");
			Print(res);
			res = CustomSerializer.Deserialize<Tablet>(Format.XML, "task1/xml.xml");
			Console.WriteLine("\nXml");
			Print(res);

			static void Print(Tablet tablet) {
				Console.WriteLine(
					$"{tablet.Brand} {tablet.Name}" +
					$"\nPrice: {tablet.Price}" +
					$"\nColor: {tablet.Color} " +
					$"\nWeight: {tablet.Weight}kg" +
					$"\nPixels amt: {tablet.PxlsAmt}"
					);
			}
		}

		static void Task2() {
			var server = new Server(3030);
			server.Start();
			var client = new Client(server.IPAddress, server.IPEndPoint);

			var data = client.Send(Encoding.UTF8.GetBytes("Дай данных"));
			var collection = JsonConvert.DeserializeObject<List<string>>(Encoding.UTF8.GetString(data));

			Console.WriteLine("Полученные данные с сервера:");
			foreach (var item in collection)
				Console.WriteLine(item);

			server.Stop();
		}

		static void Task3() {
			XmlDocument doc = new XmlDocument();
			doc.Load("task1/xml.xml");
			var root = doc.DocumentElement;
			var name = root.SelectSingleNode("Name");
			var dimensions = root.SelectSingleNode("Dimensions");
			var dimensionsItems = dimensions.SelectNodes("*");
			Console.Write($"Название: {name.InnerText}\nГабариты: ");
			foreach (var item in dimensionsItems)
				Console.Write(((XmlNode)item).InnerText + " ");
		}

		static void Task4() {
			string json = JsonConvert.SerializeObject(products);
			json = "{\"Data\": " + json + "}";
			using (var sw = new StreamWriter("task4.json")) {
				sw.Write(json);
			}
			var JParsed = JObject.Parse(json);

			// Все товары в порядке убывания цены
			Console.WriteLine("\nВсе товары");
			foreach (var item in JParsed["Data"].OrderByDescending(item => item["Price"].Value<double>()))
				Console.WriteLine($"{item["Brand"], -10} {item["Name"], -40} {item["Price"]}$");

			// Товары с ценой ниже заданой
			int price = 300;
			var fromPrice = JParsed["Data"].Where(item => item["Price"].Value<int>() < price);
			Console.WriteLine("\nТовары с ценой ниже " + price);
			foreach (var item in fromPrice)
				Console.WriteLine($"{item["Name"],-20}: {item["Price"]}$");

			// Товары заданного бренда
			string brand = "Lenovo";
			var fromBrand = JParsed["Data"].Where(item => item["Brand"].Value<string>() == brand);
			Console.WriteLine($"\nТовары {brand}");
			foreach (var item in fromBrand)
				Console.WriteLine($"{item["Name"],-20}: {item["Price"]}$");

		}
	}
}
