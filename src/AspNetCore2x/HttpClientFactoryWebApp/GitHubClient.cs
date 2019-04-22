using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryWebApp {
	public class GitHubClient {
		private readonly HttpClient _client;

		public GitHubClient(HttpClient client) {
			_client = client;
			_client.BaseAddress = new Uri("https://api.github.com");
			_client.DefaultRequestHeaders.Accept.TryParseAdd("application/vnd.github.v3+json");
		}

		public Task<Gist> GetGistAsync(string id) {
			// todo:
			return Task.FromResult(new Gist());
		}
	}
}
