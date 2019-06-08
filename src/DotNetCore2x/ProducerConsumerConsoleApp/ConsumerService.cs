using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public class ConsumerService : WorkerService {
		private readonly IBlockingQueue<IEnumerable<byte>> _queue;
		private readonly IConsoleHelper _console;

		public ConsumerService(
			IApplicationLifetime lifetime,
			IBlockingQueue<IEnumerable<byte>> queue,
			IConsoleHelper console)
			: base(lifetime) {
			_queue = queue;
			_console = console;
		}

		private void WriteLine(string message) => _console.WriteLine(message, ConsoleColor.Cyan);

		protected override async Task ExecuteCoreAsync(CancellationToken stoppingToken) {
			var random = new Random();

			while (!stoppingToken.IsCancellationRequested) {
				WriteLine($"{nameof(ConsumerService)}: Dequeue(before)");
				var bytes = await _queue.DequeueAsync(stoppingToken);
				WriteLine($"{nameof(ConsumerService)}: Dequeue(after) {HexHelper.ToString(bytes)}");

				// ランダムな時間待機する（何か処理する想定）
				var sec = random.Next(1, 3);
				WriteLine($"{nameof(ConsumerService)}: Wait {sec}s");
				await Task.Delay(TimeSpan.FromSeconds(sec), stoppingToken);
			}
		}
	}
}
