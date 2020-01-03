using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
				// クッキー認証
				.AddCookie()
				// OpenID Connectによる認証
				.AddOpenIdConnect(authenticationScheme: "Auth0", options => {
					// appsettings.jsonのAuth0オプション
					var auth0Options = _config.GetSection("Auth0").Get<Auth0Options>();

					// Authority
					options.Authority = auth0Options.Authority;

					// Client ID & Client Secret
					options.ClientId = auth0Options.ClientId;
					options.ClientSecret = auth0Options.ClientSecret;

					options.CallbackPath = new PathString("/callback");
					options.ClaimsIssuer = "Auth0";
					options.ResponseType = OpenIdConnectResponseType.Code;
					options.Scope.Add("openid");

					options.Events = new OpenIdConnectEvents {
						OnRedirectToIdentityProviderForSignOut = (context) => {

							// todo:

							return Task.CompletedTask;
						},
					};

					// todo:

				});

			// MVC
			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Default}/{action=Index}/{id?}");
			});
		}
	}
}
