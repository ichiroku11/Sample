using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCheckUiWebApp {
	// 参考
	// https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks#healthcheckui-and-failure-notifications
	// https://blog.shibayan.jp/entry/20181219/1545185333
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// todo:
			services.AddHealthChecks();

			// todo:
			services.AddHealthChecksUI();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// todo:
			app.UseHealthChecks(
				path: "/health",
				options: new HealthCheckOptions {
					// todo:
					ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
				});

			// todo:
			app.UseHealthChecksUI();

			app.Run(async (context) => {
				await context.Response.WriteAsync("Hello World!");
			});
		}
	}
}
