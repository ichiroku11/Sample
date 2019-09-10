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

namespace MultiTenantWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// Cookie認証
			services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => {
					options.LoginPath = CookieAuthenticationDefaults.LoginPath.ToString().ToLower();
				});

			// MVC
			services.AddMvc(options => {
				// グローバルフィルタで認証
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			});

			services.Configure<RouteOptions>(options => {
				// URL小文字
				options.LowercaseUrls = true;
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// 認証
			app.UseAuthentication();

			// MVC
			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Default}/{action=Index}/{id?}");
			});
		}
	}
}
