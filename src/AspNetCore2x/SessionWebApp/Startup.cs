using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SessionWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// セッションを使う
			services.AddSession(options => {
				// セッションクッキーの名前を変える
				options.Cookie.Name = "session";
			});

			services.AddMvc();

			services.Configure<RouteOptions>(options => {
				options.LowercaseUrls = true;
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// パイプラインでセッションを使う
			app.UseSession();

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Default}/{action=Index}/{id?}");
			});
		}
	}
}
