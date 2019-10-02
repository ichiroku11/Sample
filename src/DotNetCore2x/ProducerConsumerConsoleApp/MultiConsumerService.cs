using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ProducerConsumerConsoleApp {
	public class MultiConsumerService : WorkerService {
		public MultiConsumerService(IApplicationLifetime lifetime) : base(lifetime) {
		}

		protected override Task ExecuteCoreAsync(CancellationToken stoppingToken) {
			throw new NotImplementedException();
		}
	}
}
