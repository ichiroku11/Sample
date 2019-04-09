using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace HealthCheckWebApp {
	public class SampleHealthCheck : IHealthCheck {
		private readonly ILogger _logger;

		public SampleHealthCheck(ILogger<SampleHealthCheck> logger) {
			_logger = logger;
		}

		public Task<HealthCheckResult> CheckHealthAsync(
			HealthCheckContext context,
			CancellationToken cancellationToken = default) {
			_logger.LogInformation(nameof(CheckHealthAsync));

			// todo:
			return Task.FromResult(HealthCheckResult.Healthy());
		}
	}
}
