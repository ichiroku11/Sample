using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BasicAuthnWebApp.Test {
	public class BasicAuthnWebAppTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable {
		private readonly WebApplicationFactory<Startup> _factory;

		public BasicAuthnWebAppTest(WebApplicationFactory<Startup> factory) {
			_factory = factory;
		}

		public void Dispose() {
		}

		[Fact]
		public async Task DefaultController_AllowAnonymous_匿名アクセスできる() {
			// Arrange
			using var client = _factory.CreateClient();

			// Act
			using var response = await client.GetAsync("/allowanonymous");
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("AllowAnonymous", content);
		}

		[Fact]
		public async Task DefaultController_RequireAuthenticated_匿名アクセスできない() {
			// Arrange
			using var client = _factory.CreateClient();

			// Act
			using var response = await client.GetAsync("/requireauthenticated");

			// Assert
			Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
		}

		[Fact(Skip = "作成中")]
		public async Task DefaultController_RequireAuthenticated_Basic認証でアクセスできる() {
			// Arrange
			using var client = _factory.CreateClient();

			// todo: Basic認証ヘッダを

			// Act
			using var response = await client.GetAsync("/requireauthenticated");
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("RequireAuthenticated", content);
		}
	}
}
