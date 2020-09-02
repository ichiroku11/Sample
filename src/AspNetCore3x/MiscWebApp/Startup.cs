using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiscWebApp {
	public class Startup {
		private static readonly JsonSerializerOptions _jsonSerializerOptions
			= new JsonSerializerOptions {
				DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = true,
			};

		public void ConfigureServices(IServiceCollection services) {
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapGet("/connection", async context => {
					var connection = new {
						// サーバのIPアドレスとポート番号
						Local = new {
							IpAddress = context.Connection.LocalIpAddress.ToString(),
							Port = context.Connection.LocalPort
						},
						// クライアントのIPアドレスとポート番号
						Remote = new {
							IpAddress = context.Connection.RemoteIpAddress.ToString(),
							Port = context.Connection.RemotePort
						},
					};

					var json = JsonSerializer.Serialize(connection, _jsonSerializerOptions);
					await context.Response.WriteAsync(json);
				});

				endpoints.MapGet("/request/{**path}", async context => {
					var request = new {
						context.Request.Scheme,
						// リクエストのホスト名にポート番号が含まれる
						Host = context.Request.Host.Value,
						PathBase = context.Request.PathBase.Value,
						Path = context.Request.Path.Value,
						QueryString = context.Request.QueryString.Value,
					};
					var json = JsonSerializer.Serialize(request, _jsonSerializerOptions);
					await context.Response.WriteAsync(json);
				});

				endpoints.MapGet("/header", async context => {
					await context.Response.WriteAsync("Header");
				});

				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
