using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest.Http {
	public class HttpClientCookieTest : IDisposable {
		private class Startup {
			public void ConfigureServices(IServiceCollection services) {
			}

			private static void ConfigureSetCookieTest(IApplicationBuilder app) {
				app.Run(context => {
					// Set-Cookieヘッダを付与
					context.Response.Cookies.Append("abc", "xyz");
					return Task.CompletedTask;
				});
			}

			public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
				app.Map("/setcookie", ConfigureSetCookieTest);

				app.Run(context => {
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
					return Task.CompletedTask;
				});
			}
		}

		private readonly TestServer _server;

		public HttpClientCookieTest() {
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
		}

		public void Dispose() {
			_server.Dispose();
		}

		[Fact]
		public async Task GetAsync_レスポンスのSetCookieヘッダを取得する() {
			// Arrange
			using var client = _server.CreateClient();
			// Act
			var response = await client.GetAsync("/setcookie");

			var statusCode = response.StatusCode;
			var containsSetCookie = response.Headers.TryGetValues("Set-Cookie", out var cookieValues);

			// Assert
			Assert.Equal(HttpStatusCode.OK, statusCode);
			Assert.True(containsSetCookie);
			Assert.Equal("abc=xyz; path=/", cookieValues.Single());
		}
	}
}
