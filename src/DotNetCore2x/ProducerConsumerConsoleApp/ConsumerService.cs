using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public class ConsumerService : WorkerService {
		private readonly IBlockingQueue<IEnumerable<byte>> _queue;

		public ConsumerService(IApplicationLifetime lifetime, IBlockingQueue<IEnumerable<byte>> queue)
			: base(lifetime) {
			_queue = queue;
		}

		private void WriteLine(string message) => ConsoleHelper.WriteLine(message, ConsoleColor.Cyan);

		protected override async Task ExecuteCoreAsync(CancellationToken stoppingToken) {
			var random = new Random();

			while (!stoppingToken.IsCancellationRequested) {
				WriteLine($"{nameof(ConsumerService)}: Dequeue(before)");
				var bytes = await _queue.DequeueAsync(stoppingToken);
				WriteLine($"{nameof(ConsumerService)}: Dequeue(after) {HexHelper.ToString(bytes)}");

				// ランダムな時間待機する（何か処理する想定）
				var sec = random.Next(1, 5);

				WriteLine($"{nameof(ConsumerService)}: Wait {sec}s");
				await Task.Delay(TimeSpan.FromSeconds(sec));
			}
		}
	}
}
