using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
					await context.Response.WriteAsync($"home|{context.Request.GetDisplayUrl()}");
				});
			}
			private static void ConfigureApiSub(IApplicationBuilder app) {
				app.Run(async context => {
					context.Response.ContentType = "text/plain";
					await context.Response.WriteAsync($"apisub|{context.Request.GetDisplayUrl()}");
				});
			}

			public void Configure(IApplicationBuilder app) {
				app.Map("/home", ConfigureApp);
				app.Map("/api/sub", ConfigureApiSub);

				app.Run(async context => {
					context.Response.ContentType = "text/plain";
					await context.Response.WriteAsync($"root|{context.Request.GetDisplayUrl()}");
				});
			}
		}

		private static TestServer CreateServer(string baseUri)
			=> new TestServer(new WebHostBuilder().UseStartup<Startup>()) {
				BaseAddress = new Uri(baseUri)
			};

		private readonly ITestOutputHelper _output;

		public HttpClientBaseAddressTest(ITestOutputHelper output) {
			_output = output;
		}

		[Theory]
		// ドメイン直下に配置する場合は最後のスラッシュができる
		[InlineData("http://example.jp", "http://example.jp/")]
		[InlineData("http://example.jp/", "http://example.jp/")]
		// パスに配置する場合は最後のスラッシュがそのままになる
		[InlineData("http://example.jp/app", "http://example.jp/app")]
		[InlineData("http://example.jp/app/", "http://example.jp/app/")]
		public void BaseAddress_TestServerとHttpClientのBaseAddressを確認する(
			string serverBaseUri, string expectedClientBaseUri) {
			// Arrange
			// Act
			using var server = CreateServer(serverBaseUri);
			using var client = server.CreateClient();

			// Assert
			Assert.Equal(expectedClientBaseUri, client.BaseAddress.AbsoluteUri);
		}
	}
}
