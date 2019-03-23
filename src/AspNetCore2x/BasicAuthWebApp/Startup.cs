using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BasicAuthWebApp {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			// Basic認証
			services
				.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
				.AddBasic(options => {
				});

			// MVC
			services.AddMvc(options => {
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			});

			services.Configure<RouteOptions>(options => {
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
			app.UseMvcWithDefaultRoute();
		}
	}
}
