using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest.Http {
	public class HttpClientHandlerTest : IDisposable {
		private class Startup {
			public void ConfigureServices(IServiceCollection services) {
			}

			public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
				app.Run(async context => {
					context.Response.ContentType = "text/plain";
					context.Response.Cookies.Append("abc", "xyz");
					await context.Response.WriteAsync("Hello World!");
				});
			}
		}

		private readonly TestServer _server;

		public HttpClientHandlerTest() {
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
		}

		public void Dispose() {
			_server.Dispose();
		}

		[Fact]
		public async Task GetAsync_とりあえず使ってみる() {
			// Arrange
			using var handler = _server.CreateHandler();
			using var client = new HttpClient(handler) {
				BaseAddress = _server.BaseAddress,
			};

			// Act
			var response = await client.GetAsync("/");

			var statusCode = response.StatusCode;
			var contentType = response.Content.Headers.ContentType.MediaType;
			var contentText = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal(HttpStatusCode.OK, statusCode);
			Assert.Equal("text/plain", contentType);
			Assert.Equal("Hello World!", contentText);
		}
	}
}
