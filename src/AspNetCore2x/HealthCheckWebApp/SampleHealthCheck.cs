using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckWebApp {
	public class SampleHealthCheck : IHealthCheck {
		public Task<HealthCheckResult> CheckHealthAsync(
			HealthCheckContext context,
			CancellationToken cancellationToken = default) {
			// todo:
			return Task.FromResult(HealthCheckResult.Healthy());
		}
	}
}
