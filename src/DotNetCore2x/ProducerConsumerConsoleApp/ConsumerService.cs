using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public class ConsumerService : IHostedService {
		public Task StartAsync(CancellationToken cancellationToken) {
			// todo:
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			// todo:
			return Task.CompletedTask;
		}
	}
}
