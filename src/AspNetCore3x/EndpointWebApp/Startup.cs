using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EndpointWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapGet("/endpoints", async context => {
					var endpointDataSource = context.RequestServices.GetRequiredService<EndpointDataSource>();

					// エンドポイント一覧
					foreach (var endpoint in endpointDataSource.Endpoints) {
						await context.Response.WriteLineAsync(endpoint.DisplayName);
					}
				}).WithDisplayName("endpoints");

				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				}).WithDisplayName("root");
			});
		}
	}
}
