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

		protected override async Task ExecuteCoreAsync(CancellationToken stoppingToken) {
			while (!stoppingToken.IsCancellationRequested) {
				Console.WriteLine($"{nameof(ConsumerService)}: Dequeue(before)");
				var bytes = await _queue.DequeueAsync(stoppingToken);
				Console.WriteLine($"{nameof(ConsumerService)}: Dequeue(after) {HexHelper.ToString(bytes)}");
			}
		}
	}
}
