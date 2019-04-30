using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HttpClientFactoryWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// ハンドラをHttpClientに追加するために
			// ハンドラをサービスに登録
			services.AddTransient<GitHubHttpClientHandler>();

			// https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2
			// 型指定されたクライアント（Typed client）を登録
			services
				.AddHttpClient<GitHubApiClient>(client => {
					// ここでカスタマイズすることもできる
				})
				// HttpClientにハンドラを追加する
				.AddHttpMessageHandler<GitHubHttpClientHandler>();

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
