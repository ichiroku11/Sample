using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HttpsRedirectionWebApp {
	// httpからhttpsへのリダイレクトを有効にするには
	// 1. httpsリダイレクト用のミドルウェアをパイプラインに追加
	// 2. https用ポート番号を設定する
	//    a. HttpsRedirectionOptions.HttpPort
	//    b. 環境変数のASPNETCORE_HTTPS_PORT
	//    c. WebHost設定のhttps_port
	//    ※ポート番号を設定しないと警告 ”Failed to determine the https port for redirect."
	// 
	// https://docs.microsoft.com/ja-jp/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.2&tabs=visual-studio
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// IIS Expressも考慮するにはどうしたらいいか？

			// 2. https用ポート番号を設定する
			services.AddHttpsRedirection(options => {
				options.HttpsPort = 443;
			});
			// または
			/*
			services.Configure<HttpsRedirectionOptions>(options => {
				options.HttpsPort = 443;
			});
			*/
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// 1. httpsリダイレクト用のミドルウェアをパイプラインに追加
			app.UseHttpsRedirection();

			app.Run(async (context) => {
				await context.Response.WriteAsync("Hello World!");
			});
		}
	}
}
