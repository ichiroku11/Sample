using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace HealthCheckWebApp {
	public class Startup {
		private readonly ILogger _logger;

		public Startup(ILogger<Startup> logger) {
			_logger = logger;
		}

		public void ConfigureServices(IServiceCollection services) {
			// ヘルスチェックに必要なサービスを登録
			services.AddHealthChecks()
				.AddCheck<SampleHealthCheck>(
					name: "sample-check-nourishing",
					tags: new[] { "tag-nourishing" });

			services.AddHealthChecks()
				.AddCheck(
					name: "sample-check-junk",
					check: () => {
						_logger.LogInformation($"DelegateHealthCheck.{nameof(IHealthCheck.CheckHealthAsync)}");
						return HealthCheckResult.Unhealthy();
					},
					tags: new[] { "tag-junk" });

			services.AddSingleton<IHealthCheckPublisher, SampleHealthCheckPublisher>();

			services.Configure<HealthCheckPublisherOptions>(options => {
				// todo:
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseHealthChecks(
				path: "/health/nourishing",
				options: new HealthCheckOptions {
					Predicate = registration => registration.Tags.Contains("tag-nourishing"),
				});

			app.UseHealthChecks(
				path: "/health/junk",
				options: new HealthCheckOptions {
					Predicate = registration => registration.Tags.Contains("tag-junk"),
				});

			app.Run(async (context) => {
				await context.Response.WriteAsync("Hello World!");
			});
		}
	}
}
