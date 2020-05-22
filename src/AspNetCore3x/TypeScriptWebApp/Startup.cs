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

namespace TypeScriptWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddRazorPages();

			services.Configure<RouteOptions>(options => {
				options.LowercaseQueryStrings = true;
				options.LowercaseUrls = true;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapRazorPages();
			});
		}
	}
}
