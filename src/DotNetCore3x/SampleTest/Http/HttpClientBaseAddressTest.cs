using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest.Http {
	public class HttpClientBaseAddressTest {
		private class Startup {
			public void ConfigureServices(IServiceCollection services) {
			}

			private static void ConfigureApp(IApplicationBuilder app) {
				app.Run(async context => {
					context.Response.ContentType = "text/plain";
					await context.Response.WriteAsync("app");
				});
			}

			public void Configure(IApplicationBuilder app) {
				app.Map("/app", ConfigureApp);

				app.Run(async context => {
					context.Response.ContentType = "text/plain";
					await context.Response.WriteAsync("root");
				});
			}
		}

		private readonly ITestOutputHelper _output;

		public HttpClientBaseAddressTest(ITestOutputHelper output) {
			_output = output;
		}

		[Theory(DisplayName = "BaseAddressにドメイン名を指定した場合、requestUriが「/」で始まらなくても問題ない")]
		[InlineData("http://example.jp", "/app")]
		[InlineData("http://example.jp", "app")]
		// BaseAddressが「/」で終わる場合も問題ない
		[InlineData("http://example.jp/", "app")]
		[InlineData("http://example.jp/", "/app")]
		public async Task BaseAddress_ドメイン名を指定した場合(string baseUri, string requestUri) {
			// Arrange
			using var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			server.BaseAddress = new Uri(baseUri);
			using var client = server.CreateClient();

			// Act
			var response = await client.GetAsync(requestUri);
			_output.WriteLine(response.RequestMessage.RequestUri.AbsoluteUri);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal("http://example.jp/app", response.RequestMessage.RequestUri.AbsoluteUri);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("app", content);
		}
	}
}
