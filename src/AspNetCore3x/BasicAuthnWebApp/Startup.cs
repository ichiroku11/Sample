using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
					// todo:
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
				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello Basic Auth!");
				});
			});
		}
	}
}
