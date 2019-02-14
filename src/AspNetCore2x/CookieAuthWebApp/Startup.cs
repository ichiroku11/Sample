using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CookieAuthWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// クッキー認証に必要なサービスを登録
			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => {
					// クッキーの名前を変える
					options.Cookie.Name = "auth";

					// リダイレクトするログインURLも小文字に変える
					// ~/Account/Login =＞ ~/account/login
					options.LoginPath = CookieAuthenticationDefaults.LoginPath.ToString().ToLower();
				});

			// MVCで利用するサービスを登録
			services.AddMvc(options => {
				// グローバルフィルタに承認フィルタを追加
				// すべてのコントローラでログインが必要にしておく
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			});

			services.Configure<RouteOptions>(options => {
				// URLは小文字にする
				options.LowercaseUrls = true;
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// パイプラインに認証のミドルウェアを追加する
			// HttpContext.Userをセットしてくれる
			app.UseAuthentication();

			// パイプラインにMVCのミドルウェアを追加する
			app.UseMvcWithDefaultRoute();
		}
	}
}
