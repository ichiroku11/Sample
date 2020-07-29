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
			private static void ConfigureApiSub(IApplicationBuilder app) {
				app.Run(async context => {
					context.Response.ContentType = "text/plain";
					await context.Response.WriteAsync("apisub");
				});
			}

			public void Configure(IApplicationBuilder app) {
				app.Map("/app", ConfigureApp);
				app.Map("/api/sub", ConfigureApiSub);

				app.Run(async context => {
					context.Response.ContentType = "text/plain";
					await context.Response.WriteAsync("root");
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

		[Theory(DisplayName = "BaseAddressにドメイン名を指定した場合、requestUriが「/」で始まっても始まらなくてもreuqestUriの相対パスのURLへのリクエストになる")]
		[InlineData("http://example.jp", "app")]
		[InlineData("http://example.jp", "/app")]
		// BaseAddressが「/」で終わる場合も問題ない
		[InlineData("http://example.jp/", "app")]
		[InlineData("http://example.jp/", "/app")]
		public async Task BaseAddress_ドメイン名を指定した場合(string baseUri, string requestUri) {
			// Arrange
			using var server = CreateServer(baseUri);
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

		[Theory(DisplayName = "BaseAddressにパスが含まれてrequestUriが「/」で始まる場合は、reuqestUriの相対パスのURLへのリクエストになる")]
		[InlineData("http://example.jp/app", "/", "http://example.jp/", "root")]
		[InlineData("http://example.jp/app/", "/", "http://example.jp/", "root")]
		[InlineData("http://example.jp/app/path1/path2/", "/", "http://example.jp/", "root")]
		[InlineData("http://example.jp/app/path1/path2/", "/app", "http://example.jp/app", "app")]
		public async Task BaseAddress_パスを含めた場合でリクエストURIが相対パス(
			string baseUri, string requestUri, string expectedUri, string expectedContent) {
			// Arrange
			using var server = CreateServer(baseUri);
			using var client = server.CreateClient();

			// Act
			var response = await client.GetAsync(requestUri);
			var actualUri = response.RequestMessage.RequestUri.AbsoluteUri;
			var actualContent = await response.Content.ReadAsStringAsync();

			// Assert
			_output.WriteLine(actualUri);
			Assert.Equal(expectedUri, actualUri);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(expectedContent, actualContent);
		}

		// todo
		[Theory(DisplayName = "BaseAddressにパスが含まれてrequestUriが「/」で始まらない場もreuqestUriの相対パスのURLへのリクエストになる")]
		[InlineData("http://example.jp/xyz", "app", "http://example.jp/app", "app")]
		[InlineData("http://example.jp/api/", "sub", "http://example.jp/api/sub", "root")]
		[InlineData("http://example.jp/app", "api/sub", "http://example.jp/api/sub", "apisub")]
		[InlineData("http://example.jp/app/path1/path2/", "api/sub", "http://example.jp/app/path1/path2/api/sub", "apisub")]
		public async Task BaseAddress_パスを含めた場合でリクエストURIがスラッシュで始まらない(
			string baseUri, string requestUri, string expectedUri, string expectedContent) {
			// Arrange
			using var server = CreateServer(baseUri);
			using var client = server.CreateClient();

			// Act
			var response = await client.GetAsync(requestUri);
			var actualUri = response.RequestMessage.RequestUri.AbsoluteUri;
			var actualContent = await response.Content.ReadAsStringAsync();

			// Assert
			_output.WriteLine(actualUri);
			Assert.Equal(expectedUri, actualUri);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(expectedContent, actualContent);
		}

		[Theory(DisplayName = "requestUriが空文字列だとルートへのリクエストになる")]
		// ドメイン名だけの場合、取得できるRequestUriの最後に「/」になる
		[InlineData("http://example.jp", "http://example.jp/", "root")]
		[InlineData("http://example.jp/", "http://example.jp/", "root")]
		// ドメイン名とパスの場合、取得できるRequestUriの最後に「/」がつかず、baseUriのまま
		[InlineData("http://example.jp/app", "http://example.jp/app", "root")]
		[InlineData("http://example.jp/app/", "http://example.jp/app/", "root")]
		public async Task BaseAddress_リクエストURIが空文字列(
			string baseUri, string expectedUri, string expectedContent) {
			// Arrange
			using var server = CreateServer(baseUri);
			using var client = server.CreateClient();

			// Act
			var response = await client.GetAsync("");
			var actualUri = response.RequestMessage.RequestUri.AbsoluteUri;
			var actualContent = await response.Content.ReadAsStringAsync();

			// Assert
			_output.WriteLine(actualUri);
			Assert.Equal(expectedUri, actualUri);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(expectedContent, actualContent);
		}
	}
}
