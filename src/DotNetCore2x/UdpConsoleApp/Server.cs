﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpConsoleApp {
	// サーバ
	public class Server<TRequest, TResponse> {
		// リクエストを受信するエンドポイント
		private readonly IPEndPoint _endpoint;

		public Server(IPEndPoint endpoint) {
			_endpoint = endpoint;
		}

		// サーバを実行する
		public async Task RunAsync(Func<TRequest, TResponse> processor) {
			// リクエストを受信するエンドポイントを指定してUdpClientを作成
			using (var client = new UdpClient(_endpoint)) {
				while (true) {
					// 1. リクエストを受信
					var result = await client.ReceiveAsync();
					var requestBytes = result.Buffer;
					var request = new ObjectConverter<TRequest>().FromByteArray(requestBytes);
					Console.WriteLine($"Server receive {nameof(request)}: {request}");

					var sender = Task.Run(async () => {
						// 2. リクエストからレスポンスを作成
						var response = processor(request);

						// 3. リクエストの送信元にレスポンスを送信
						Console.WriteLine($"Server send {nameof(response)}: {response}");
						var responseBytes = new ObjectConverter<TResponse>().ToByteArray(response);
						await client.SendAsync(responseBytes, responseBytes.Length, result.RemoteEndPoint);
					});

					// Taskの管理やエラー処理は省略
				}
			}
		}
	}
}
