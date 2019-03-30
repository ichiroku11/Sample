using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CookieAuthWebApp.Test {
	public class CookieAuthWebAppTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable {
		private static HttpContent GetEmptyContent()
			=> new FormUrlEncodedContent(new Dictionary<string, string>());

		private readonly WebApplicationFactory<Startup> _factory;

		public CookieAuthWebAppTest(WebApplicationFactory<Startup> factory) {
			_factory = factory;
		}

		private Uri BaseAddress => _factory.Server.BaseAddress;

		public void Dispose() {
			_factory.Dispose();
		}

		[Fact]
		public async Task HomeIndex_GETリクエストを送るとログインページへリダイレクト() {
			var options = new WebApplicationFactoryClientOptions {
				AllowAutoRedirect = false,
			};
			using (var clinet = _factory.CreateClient(options)) {
				var response = await clinet.GetAsync("/");

				Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
				Assert.Equal(
					new Uri(BaseAddress, "/account/login").ToString(),
					response.Headers.Location.GetLeftPart(UriPartial.Path));
			}
		}
	}
}
