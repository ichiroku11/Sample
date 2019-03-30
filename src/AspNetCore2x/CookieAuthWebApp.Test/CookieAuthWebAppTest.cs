using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CookieAuthWebApp.Test {
	public class CookieAuthWebAppTest : IClassFixture<WebApplicationFactory<Startup>> {
		private readonly WebApplicationFactory<Startup> _factory;

		public CookieAuthWebAppTest(WebApplicationFactory<Startup> factory) {
			_factory = factory;
		}

		[Fact]
		public async Task HomeIndex_Get_AccountLoginへリダイレクト() {
			var options = new WebApplicationFactoryClientOptions {
				AllowAutoRedirect = false,
			};
			using (var clinet = _factory.CreateClient(options)) {
				var response = await clinet.GetAsync("/");

				Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
				Assert.Equal(
					new Uri(_factory.Server.BaseAddress, "/account/login").ToString(),
					response.Headers.Location.GetLeftPart(UriPartial.Path));
			}
		}

		[Fact]
		public async Task AccountLogin_Post_HomeIndexへリダイレクト() {
			var options = new WebApplicationFactoryClientOptions {
				AllowAutoRedirect = false,
			};
			using (var clinet = _factory.CreateClient(options)) {
				var response = await clinet.PostAsync(
					"/account/login",
					new FormUrlEncodedContent(new Dictionary<string, string>()));

				Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
				Assert.Equal("/", response.Headers.Location.ToString());
			}
		}

		[Fact]
		public async Task AccountLogin_Post_HomeIndexへリダイレクトして取得できる() {
			using (var clinet = _factory.CreateClient()) {
				var response = await clinet.PostAsync(
					"/account/login",
					new FormUrlEncodedContent(new Dictionary<string, string>()));
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);

				var content = await response.Content.ReadAsStringAsync();
				Assert.Equal("user01@example.jp", content);
			}
		}
	}
}
