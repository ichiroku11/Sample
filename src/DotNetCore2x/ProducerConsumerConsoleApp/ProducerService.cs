using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public class ProducerService : WorkerService {
		private readonly IBlockingQueue<IEnumerable<byte>> _queue;

		public ProducerService(IApplicationLifetime lifetime, IBlockingQueue<IEnumerable<byte>> queue)
			: base(lifetime) {
			_queue = queue;
		}

		protected override async Task ExecuteCoreAsync(CancellationToken stoppingToken) {
			while (!stoppingToken.IsCancellationRequested) {
				// todo: ランダムな時間待機する
				await Task.Delay(1000, stoppingToken);

				// todo: ランダムな値を生成してキューに追加する
				var item = new byte[] {
					0x00, 0x01, 0x02, 0x03, 0x04,
				};
				_queue.Enqueue(item);
			}
		}
	}
}
