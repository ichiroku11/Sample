using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Auth0WebApp {
	// 参考
	// https://auth0.com/docs/quickstart/webapp/aspnet-core-3
	public class Startup {
		private readonly IConfiguration _config;

		public Startup(IConfiguration config) {
			_config = config;
		}

		public void ConfigureServices(IServiceCollection services) {
			// 認証
			services
				// 認証サービスを追加
				.AddAuthentication(options => {
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				// クッキー認証ハンドラを追加
				.AddCookie(options => {
					options.Cookie.Name = "auth";

					options.Events = new LoggingCookieAuthenticationEvents();
				})
				// OpenID Connect認証ハンドラを追加
				.AddOpenIdConnect(Auth0Defaults.AuthenticationScheme, options => {
					// appsettings.jsonのAuth0オプション
					var auth0Options = _config.GetSection("Auth0").Get<Auth0Options>();

					// Authority
					options.Authority = auth0Options.Authority;

					// Client ID & Client Secret
					options.ClientId = auth0Options.ClientId;
					options.ClientSecret = auth0Options.ClientSecret;

					// CallbackPathのデフォルト値はこれ
					// Auth0側で「Allowed Callback URLs」でこのURLを指定すればOK
					//options.CallbackPath = new PathString("/signin-oidc");

					// todo:
					// SignedOutCallbackPathのデフォルト値はこれ
					// でも使っていないっぽい
					//options.SignedOutCallbackPath = new PathString("/signout-callback-oidc");

					options.ClaimsIssuer = "Auth0";

					options.ResponseType = OpenIdConnectResponseType.Code;

					// デフォルトで"openid"と"profile"が追加されているっぽいから不要
					//options.Scope.Add("openid");

					options.Events = new LoggingOpenIdConnectEvents {
						/*
						OnRedirectToIdentityProvider = (context) => {
							return Task.CompletedTask;
						},
						*/
						OnRedirectToIdentityProviderForSignOut = (context) => {
							var request = context.Request;

							// Auth0をログアウトしてから戻ってくるこのアプリURL
							var returnToUri = UriHelper.BuildAbsolute(
								request.Scheme,
								request.Host,
								request.PathBase,
								context.Properties.RedirectUri);
							// todo: ↑options.SignedOutCallbackPathもありか？

							var queryString = QueryString
								.Create("client_id", auth0Options.ClientId)
								.Add("returnTo", returnToUri);

							// Auth0のログアウトURL
							var logoutUri = $"{auth0Options.Authority}/v2/logout{queryString}";

							context.Response.Redirect(logoutUri);
							context.HandleResponse();

							return Task.CompletedTask;
						},
					};

					// todo:

				});

			services.AddScoped<IClaimsTransformation, LoggingClaimsTransformation>();

			// MVC
			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Default}/{action=Index}/{id?}");
			});
		}
	}
}
