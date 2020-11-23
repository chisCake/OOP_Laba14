using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Newtonsoft.Json;

namespace OOP_Laba14 {
	class Server {
		public Socket Socket { get; private set; }
		public IPAddress IPAddress { get; private set; }
		public IPEndPoint IPEndPoint { get; private set; }
		public bool IsLive { get; private set; }

		private Thread ServerThread { get; set; }

		private List<string> Data = new List<string>() {
			"Строка 1",
			"Строка 2",
			"Строка 3",
			"И другие данные"
		};

		public Server(int port) {
			IPAddress = Dns.GetHostEntry("localhost").AddressList[0];
			IPEndPoint = new IPEndPoint(IPAddress, port);
			Socket = new Socket(IPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			Socket.Bind(IPEndPoint);
		}

		public void Start() {
			IsLive = true;
			ServerThread = new Thread(new ThreadStart(StartThread));
			ServerThread.Start();
		}

		public void Stop() {
			IsLive = false;
			try {
				ServerThread.Abort();
				Socket.Close();
			}
			catch { }
		}

		private void StartThread() {
			try {
				Socket.Listen(5);

				while (IsLive) {
					Socket handler = Socket.Accept();
					var data = JsonConvert.SerializeObject(Data);
					var bytes = Encoding.UTF8.GetBytes(data);
					handler.Send(bytes);
				}
			}
			catch { }
		}
	}
}