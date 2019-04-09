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

namespace HealthCheckWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// ヘルスチェックに必要なサービスを登録
			services.AddHealthChecks()
				.AddCheck<SampleHealthCheck>(name: "sample-check");

			services.AddSingleton<IHealthCheckPublisher, SampleHealthCheckPublisher>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// todo:
			app.UseHealthChecks(path: "/health");

			app.Run(async (context) => {
				await context.Response.WriteAsync("Hello World!");
			});
		}
	}
}
