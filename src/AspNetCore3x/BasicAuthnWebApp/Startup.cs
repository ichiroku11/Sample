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

namespace BasicAuthnWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// todo:
			// Basic認証
			/*
			services.AddAuthentication()
				.AddBasic(_ => {
				});
			*/

			// MVC（コントローラ）
			services.AddControllers();

			services.Configure<RouteOptions>(options => {
				options.LowercaseQueryStrings = true;
				options.LowercaseUrls = true;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
