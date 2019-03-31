using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BasicAuthWebApp.Test {
	public class BasicAuthWebAppTest : IClassFixture<WebApplicationFactory<Startup>> {
		private readonly WebApplicationFactory<Startup> _factory;

		public BasicAuthWebAppTest(WebApplicationFactory<Startup> factory) {
			_factory = factory;
		}

		[Fact]
		public async Task HomeIndex_Get_Unauthorized() {
			using (var clinet = _factory.CreateClient()) {
				var response = await clinet.GetAsync("/");

				Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
			}
		}
	}
}
