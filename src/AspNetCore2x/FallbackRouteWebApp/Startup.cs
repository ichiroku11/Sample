using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FallbackRouteWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc();

			services.Configure<RouteOptions>(options => {
				options.LowercaseUrls = true;
				options.LowercaseQueryStrings = true;
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			app.UseMvc(routes => {
				// デフォルトルート
				routes.MapRoute(
					name: "default",
					template: "{controller=Default}/{action=Index}/{id?}");

				// フォールバックルート
				routes.MapRoute(
					name: "fallback",
					template: "{*url}",
					defaults: new {
						controller = "Error",
						action = "NotFound"
					});
			});

		}
	}
}
