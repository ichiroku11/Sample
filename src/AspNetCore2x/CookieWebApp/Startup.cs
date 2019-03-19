using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CookieWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc();

			services.Configure<RouteOptions>(options => {
				options.LowercaseUrls = true;
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Default}/{action=Index}/{id?}");
			});
		}
	}
}
