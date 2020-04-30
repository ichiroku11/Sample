using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTest.AspNetCore {
	public static class HttpRequestExtensions {
		private const string _schemeDelimiter = "://";

		public static string GetAppUrl(this HttpRequest request) {
			var scheme = request.Scheme ?? "";
			var host = request.Host.Value ?? "";
			var pathBase = request.PathBase.Value ?? "";

			return new StringBuilder()
				.Append(scheme)
				.Append(_schemeDelimiter)
				.Append(host)
				.Append(pathBase)
				.ToString();
		}

		public static bool IsAjax(this HttpRequest request) {
			// todo:
			return true;
		}
	}
}
