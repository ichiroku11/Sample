using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace SendGridWebApp {
	public class Startup {
		private readonly IConfiguration _config;
		public Startup(IConfiguration config) {
			_config = config;
		}

		public void ConfigureServices(IServiceCollection services) {
			services.Configure<SendGridOptions>(_config.GetSection("SendGrid"));

			services
				.AddScoped<SendGridSendTextSample>()
				.AddScoped<SendGridSendHtmlSample>()
				.AddScoped<SendGridSendTemplateSample>()
				.AddScoped<SendGridGetTemplateListSample>()
				.AddScoped<SendGridGetTemplateSample>();
		}

		private static RequestDelegate CreateDelegate<TSample>() where TSample : SendGridSample {
			return async context => {
				var sample = context.RequestServices.GetRequiredService<TSample>();
				await sample.RunAsync(context);
			};
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapGet("/send/text", CreateDelegate<SendGridSendTextSample>());
				endpoints.MapGet("/send/html", CreateDelegate<SendGridSendHtmlSample>());
				endpoints.MapGet("/send/template", CreateDelegate<SendGridSendTemplateSample>());
				endpoints.MapGet("/template/{id}", CreateDelegate<SendGridGetTemplateSample>());
				endpoints.MapGet("/template", CreateDelegate<SendGridGetTemplateListSample>());
				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello SendGrid!");
				});
			});
		}
	}
}
