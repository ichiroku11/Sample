using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HttpClientFactoryWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2
			// 型指定されたクライアント（Typed client）を登録
			services.AddHttpClient<GitHubClient>(client => {
				// ここでカスタマイズすることもできる
			});

			services.AddMvc();
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
