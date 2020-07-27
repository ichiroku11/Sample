using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BasicAuthnWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// 認証
			services
				.AddAuthentication(options => {
					options.DefaultScheme = BasicAuthenticationDefaults.AuthenticationScheme;
				})
				// Basic認証ハンドラ
				.AddBasic(_ => {
				});

			// 承認
			services
				.AddAuthorization(options => {
					// 認証ポリシー
					options.AddPolicy("Authenticated", builder => {
						builder.RequireAuthenticatedUser();
					});
				});

			// MVC（コントローラのみ）
			services.AddControllers(options => {
				// グローバルフィルタで認証必須に
				options.Filters.Add(new AuthorizeFilter("Authenticated"));
			});

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{action}/{id?}",
					defaults: new { controller = "Default" });
			});
		}
	}
}
