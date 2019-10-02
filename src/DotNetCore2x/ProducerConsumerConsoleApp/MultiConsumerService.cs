using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ProducerConsumerConsoleApp {
	public class MultiConsumerService : WorkerService {
		private readonly IBlockingQueue<IEnumerable<byte>> _queue;
		private readonly IConsoleHelper _console;

		public MultiConsumerService(
			IApplicationLifetime lifetime,
			IBlockingQueue<IEnumerable<byte>> queue,
			IConsoleHelper console)
			: base(lifetime) {
			_queue = queue;
			_console = console;
		}

		private void WriteLine(string message) => _console.WriteLine(message, ConsoleColor.Cyan);

		protected override Task ExecuteCoreAsync(CancellationToken stoppingToken) {
			throw new NotImplementedException();
		}
	}
}
