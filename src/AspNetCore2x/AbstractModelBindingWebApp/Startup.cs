using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AbstractModelBindingWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// MVCで利用するサービスを登録する
			services.AddMvc(options => {
				// todo:
				//options.ModelBinderProviders.Insert(0, new GeometryModelBinderProvider());
			}).SetCompatibilityVersion(CompatibilityVersion.Latest);

			services.Configure<RouteOptions>(options => {
				// URLは小文字に
				options.LowercaseUrls = true;
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// パイプラインにMVCのミドルウェアを追加する
			app.UseMvc(routes => {
				routes.MapRoute(
					name: "geometry",
					template: "{controller=Geometry}/{Action=index}/{id?}");
			});
		}
	}
}
