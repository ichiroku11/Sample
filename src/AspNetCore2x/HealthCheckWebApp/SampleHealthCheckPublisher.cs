using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckWebApp {
	public class SampleHealthCheckPublisher : IHealthCheckPublisher {
		private readonly ILogger _logger;

		public SampleHealthCheckPublisher(ILogger<SampleHealthCheckPublisher> logger) {
			_logger = logger;
		}

		public Task PublishAsync(HealthReport report, CancellationToken cancellationToken) {
			// todo:
			_logger.LogInformation(nameof(PublishAsync));
			_logger.LogInformation(JsonConvert.SerializeObject(report));

			return Task.CompletedTask;
		}
	}
}
