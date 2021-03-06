﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UdpConsoleApp {
	class Program {
		// サーバの作成
		public static Server<string, string> Server(IPEndPoint endpoint)
			=> new Server<string, string>(endpoint);

		// クライアントの作成
		public static Client<string, string> Client(IPEndPoint endpoint)
			=> new Client<string, string>(endpoint);

		// 文字列を並びを反対にする
		private static string Reverse(string original)
			=> new string(original.Reverse().ToArray());

		static void Main(string[] args) {
			var endpoint = new IPEndPoint(IPAddress.Loopback, 54321);

			// サーバを実行
			var server = Task.Run(() => Server(endpoint).RunAsync(Reverse));

			// クライアントを実行
			Task.WaitAll(
				Client(endpoint).SendAsync("あいうえお"),
				Client(endpoint).SendAsync("かきくけこ"));

			// サーバのTaskをきれいに終了するにはどうしたらいいのか...
		}
	}
}
