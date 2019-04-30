using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryWebApp {
	// GitHub API呼び出し
	public class GitHubApiClient {
		private readonly HttpClient _client;

		public GitHubApiClient(HttpClient client) {
			_client = client;
			_client.BaseAddress = new Uri("https://api.github.com");

			// ユーザエージェントは必須らしい
			// https://developer.github.com/v3/#user-agent-required
			_client.DefaultRequestHeaders.UserAgent.TryParseAdd("Sample");
			//_client.DefaultRequestHeaders.Accept.TryParseAdd("application/vnd.github.v3+json");
		}

		// 指定したIDのgistを取得
		public async Task<Gist> GetGistAsync(string gistId) {
			var request = new HttpRequestMessage(HttpMethod.Get, $"/gists/{gistId}");

			var response = await _client.SendAsync(request);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<Gist>(content);
		}
	}
}
