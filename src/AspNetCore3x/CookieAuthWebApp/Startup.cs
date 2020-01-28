using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CookieAuthWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services
				// 認証サービスを追加
				// 戻り値はAuthenticationBuilder
				.AddAuthentication(options => {
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				// クッキー認証ハンドラを追加
				.AddCookie(options => {
					options.Cookie.Name = "auth";

					options.Events = new LoggingCookieAuthenticationEvents();
				});

			services.AddScoped<IClaimsTransformation, LoggingClaimsTransformation>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				// 参考
				// https://qiita.com/masakura/items/85c59e60cac7f0638c1b

				endpoints.MapGet("/challenge", async context => {
					// ログインURL（CookieAuthenticationOptions.LoginPath）へのリダイレクト（302）を返す
					// 401 Unauthorizedを返すようなイメージだと思う
					await context.ChallengeAsync();
				});

				endpoints.MapGet("/forbid", async context => {
					// アクセス禁止URL（CookieAuthenticationOptions.AccessDeniedPath）へのリダイレクト（302）を返す
					// 403 Forbiddenを返すようなイメージだと思う
					await context.ForbidAsync();
				});

				endpoints.MapGet("/signin", async context => {
					// とあるユーザでログインしたとする

					// プリンシパルを作成
					// authenticationTypeを指定しないとIsAuthenticatedがfalseになり、SignInAsyncで例外が発生する
					// https://docs.microsoft.com/ja-jp/dotnet/core/compatibility/aspnetcore#identity-signinasync-throws-exception-for-unauthenticated-identity
					var identity = new ClaimsIdentity(authenticationType: "Test");
					identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1"));
					var principal = new ClaimsPrincipal(identity);

					// サインイン
					// 認証クッキーをつけたレスポンスを返す
					await context.SignInAsync(principal);
					// Set-Cookie: auth=***;
				});

				endpoints.MapGet("/signout", async context => {
					// サインアウト
					// 認証クッキーをクリアするレスポンスを返す
					await context.SignOutAsync();
					// Set-Cookie: auth=; expires=Thu, 01 Jan 1970 00:00:00 GMT;
				});

				endpoints.MapGet("/authenticate", async context => {
					// 認証クッキーからプリンシパルを作成する
					// UseAuthentication/AuthenticationMiddlewareでやってくれてること

					var result = await context.AuthenticateAsync();
					var principal = result.Principal;
					await context.Response.WriteAsync($"{nameof(result.Succeeded)}: {result.Succeeded}");
					await context.Response.WriteAsync(Environment.NewLine);
					await context.Response.WriteAsync($"{nameof(ClaimTypes.NameIdentifier)}: ");
					await context.Response.WriteAsync(principal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");

					// 認証した（~/sigininにリクエストを送った）後だと
					// Succeeded: True
					// NameIdentifier: 1
					// 認証していないと
					// Succeeded: False
					// NameIdentifier: 
				});

				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
