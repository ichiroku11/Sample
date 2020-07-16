using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ModelBindingWebApp.Controllers.Test {
	public class ValueTypeControllerTest : ControllerTestBase {
		public ValueTypeControllerTest(
			ITestOutputHelper output,
			WebApplicationFactory<Startup> factory)
			: base(output, factory) {
		}

		[Fact]
		public async Task GetWithQuery_クエリ文字列を省略するとintは0になる() {
			// Arrange
			using var request = new HttpRequestMessage(HttpMethod.Get, $"/valuetype/getwithquery");

			// Act
			using var response = await SendAsync(request);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal("0", content);
		}

		[Fact]
		public async Task GetWithRoute_ルートのパラメータを省略するとNotFound() {
			// Arrange
			using var request = new HttpRequestMessage(HttpMethod.Get, $"/valuetype/getwithroute");

			// Act
			using var response = await SendAsync(request);

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
	}
}
