using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public interface IBlockingQueue<TItem> {
		// キューに追加する
		void Enqueue(TItem item);
		// キューから取り出す
		Task<TItem> DequeueAsync(CancellationToken token = default);
	}
}
