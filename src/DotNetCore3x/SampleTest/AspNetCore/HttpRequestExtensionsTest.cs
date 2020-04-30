using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest.AspNetCore {
	public class HttpRequestExtensionsTest {
		private static HttpRequest CreateRequest(string scheme, string host, string pathBase) {
			var context = new DefaultHttpContext();

			var request = context.Request;
			request.Scheme = scheme;
			request.Host = new HostString(host);
			request.PathBase = new PathString(pathBase);

			return request;
		}

		private readonly ITestOutputHelper _output;

		public HttpRequestExtensionsTest(ITestOutputHelper output) {
			_output = output;
		}

		[Theory]
		[InlineData("https", "example.jp", null, "https://example.jp")]
		[InlineData("https", "example.jp", "/app", "https://example.jp/app")]
		public void GetAppUrl_Test(string scheme, string host, string pathBase, string expected) {
			// Arrange
			var request = CreateRequest(scheme, host, pathBase);

			// Act
			var actual = request.GetAppUrl();
			_output.WriteLine(actual);

			// Assert
			Assert.Equal(expected, actual, StringComparer.OrdinalIgnoreCase);
		}
	}
}
