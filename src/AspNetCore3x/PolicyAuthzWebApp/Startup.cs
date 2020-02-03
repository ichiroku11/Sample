using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PolicyAuthzWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// 認証
			services
				.AddAuthentication(options => {
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				}).AddCookie(options => {
				});

			// 承認
			services.AddAuthorization(options => {
				options.AddPolicy("Authenticated", builder => {
					builder.RequireAuthenticatedUser();
				});
				options.AddPolicy("AreaRoles", builder => {
					builder.RequireAreaRoles(area: "Admin", roles: "admin");
				});
			});

			// MVC
			services.AddControllers(options => {
				options.Filters.Add(new AuthorizeFilter("Authenticated"));
				options.Filters.Add(new AuthorizeFilter("AreaRoles"));
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			// 認証
			app.UseAuthentication();

			/*
			// 承認
			// AuthorizeAttributeのために必要
			app.UseAuthorization();
			*/

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Default}/{action=Index}/{id?}");
			});
		}
	}
}
