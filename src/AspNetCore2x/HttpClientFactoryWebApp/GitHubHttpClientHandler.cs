using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientFactoryWebApp {
	public class GitHubHttpClientHandler : DelegatingHandler {
		private readonly ILogger _logger;

		public GitHubHttpClientHandler(ILogger<GitHubHttpClientHandler> logger) {
			_logger = logger;
		}

		protected override Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request, CancellationToken cancellationToken) {
			// SendAsync前後に何か処理を行うとか
			try {
				_logger.LogInformation($"{nameof(SendAsync)} before");

				return base.SendAsync(request, cancellationToken);

			} finally {
				_logger.LogInformation($"{nameof(SendAsync)} after");
			}
		}
	}
}
