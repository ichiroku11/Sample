using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public class BlockingQueue<TItem> : IBlockingQueue<TItem> {
		public void Enqueue(TItem item) {
			// todo:
			throw new NotImplementedException();
		}
		public Task<TItem> DequeueAsync(CancellationToken token = default) {
			// todo:
			throw new NotImplementedException();
		}

	}
}
