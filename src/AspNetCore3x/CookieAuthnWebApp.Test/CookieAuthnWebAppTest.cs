using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CookieAuthnWebApp.Test {
	public class CookieAuthnWebAppTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable {
		private readonly WebApplicationFactory<Startup> _factory;

		public CookieAuthnWebAppTest(WebApplicationFactory<Startup> factory) {
			_factory = factory;
		}

		public void Dispose() {
			_factory.Dispose();
		}

		[Fact]
		public async Task GetChallenge_AccountLoginへのリダイレクト() {
			// Arrange
			var options = new WebApplicationFactoryClientOptions {
				AllowAutoRedirect = false,
			};
			var client = _factory.CreateClient(options);

			// Act
			var response = await client.GetAsync("/challenge");

			// Assert
			Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
			Assert.Equal(
				new Uri(_factory.Server.BaseAddress, "/account/login").ToString(),
				response.Headers.Location.GetLeftPart(UriPartial.Path));
		}
	}
}
