using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MiscWebApp.Test {
	public class HeaderTest : TestBase {
		public HeaderTest(ITestOutputHelper output, WebApplicationFactory<Startup> factory)
			: base(output, factory) {
		}

		[Fact]
		public async Task OKレスポンスを取得できる() {
			// Arrange
			var request = new HttpRequestMessage(HttpMethod.Get, "/header");

			// Act
			var response = await SendAsync(request);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}
}
