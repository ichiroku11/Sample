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
	}
}
