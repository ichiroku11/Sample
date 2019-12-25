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
				// 認証に必要なサービスを追加
				// 戻り値はAuthenticationBuilder
				.AddAuthentication(options => {
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				// クッキーを使った認証に必要なサービスを追加
				.AddCookie(options => {
					options.Cookie.Name = "auth";
				});
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
					// todo: Comment
					await context.ChallengeAsync();
				});

				endpoints.MapGet("/forbid", async context => {
					// todo: Comment
					await context.ForbidAsync();
				});

				endpoints.MapGet("/signin", async context => {
					// todo: Comment
					// authenticationTypeを指定しないとSignInAsyncで例外が
					var identity = new ClaimsIdentity(authenticationType: "Test");
					identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1"));
					identity.AddClaim(new Claim(ClaimTypes.Name, "Taro"));
					var principal = new ClaimsPrincipal(identity);

					// todo: Comment
					await context.SignInAsync(principal);
				});

				endpoints.MapGet("/signout", async context => {
					// todo: Comment
					await context.SignOutAsync();
				});

				endpoints.MapGet("/authenticate", async context => {
					// todo: Comment
					// todo: AuthenticationMiddleware
					// todo: UseAuthentication
					var result = await context.AuthenticateAsync();

					var principal = result.Principal;
					await context.Response.WriteAsync($"{nameof(ClaimTypes.NameIdentifier)}:");
					await context.Response.WriteAsync(principal?.FindFirstValue(ClaimTypes.NameIdentifier));
					await context.Response.WriteAsync(Environment.NewLine);
					await context.Response.WriteAsync($"{nameof(ClaimTypes.Name)}:");
					await context.Response.WriteAsync(principal?.FindFirstValue(ClaimTypes.Name));
				});

				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
