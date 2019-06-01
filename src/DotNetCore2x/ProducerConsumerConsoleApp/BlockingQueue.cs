using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public class BlockingQueue<TItem> : IBlockingQueue<TItem> {
		private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);
		private readonly ConcurrentQueue<TItem> _queue = new ConcurrentQueue<TItem>();

		public void Enqueue(TItem item) {
			// キューに追加する
			_queue.Enqueue(item);

			// セマフォを解放して通知する
			_semaphore.Release();
		}

		public async Task<TItem> DequeueAsync(CancellationToken token = default) {
			// セマフォが解放されるまで待機する
			await _semaphore.WaitAsync(token);

			// キューから取り出す
			_queue.TryDequeue(out var item);

			return item;
		}
	}
}
