using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auth0WebApp {
	// 参考
	// https://auth0.com/docs/quickstart/webapp/aspnet-core-3
	public class Startup {
		private readonly IConfiguration _config;

		public Startup(IConfiguration config) {
			_config = config;
		}

		public void ConfigureServices(IServiceCollection services) {
			services
				// 認証サービスを追加
				.AddAuthentication(options => {
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				// クッキー認証
				.AddCookie()
				// OpenID Connectによる認証
				.AddOpenIdConnect(authenticationScheme: "Auth0", options => {
					var auth0Options = _config.GetSection("Auth0").Get<Auth0Options>();

					options.Authority = auth0Options.Authority;
					options.ClientId = auth0Options.ClientId;
					options.ClientSecret = auth0Options.ClientSecret;
					// todo:


				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
