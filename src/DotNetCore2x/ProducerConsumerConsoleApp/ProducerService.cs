using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public class ProducerService : WorkerService {
		private readonly IBlockingQueue<IEnumerable<byte>> _queue;
		private readonly IConsoleHelper _console;

		public ProducerService(
			IApplicationLifetime lifetime,
			IBlockingQueue<IEnumerable<byte>> queue,
			IConsoleHelper console)
			: base(lifetime) {
			_queue = queue;
			_console = console;
		}

		private void WriteLine(string message) => _console.WriteLine(message, ConsoleColor.Green);

		protected override async Task ExecuteCoreAsync(CancellationToken stoppingToken) {
			var random = new Random();

			while (!stoppingToken.IsCancellationRequested) {
				// ランダムな時間待機する（ランダムに要求が発生する想定）
				var sec = random.Next(1, 5);
				WriteLine($"{nameof(ProducerService)}: Wait {sec}s");
				await Task.Delay(TimeSpan.FromSeconds(sec), stoppingToken);

				// ランダムな値をキューに追加する
				var bytes = new byte[5];
				random.NextBytes(bytes);

				WriteLine($"{nameof(ProducerService)}: Enqueue(before) {HexHelper.ToString(bytes)}");
				_queue.Enqueue(bytes);
				WriteLine($"{nameof(ProducerService)}: Enqueue(after) {HexHelper.ToString(bytes)}");
			}
		}
	}
}
