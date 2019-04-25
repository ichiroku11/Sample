using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactoryWebApp.Controllers {
	public class DefaultController : Controller {
		private readonly GitHubClient _client;

		public DefaultController(GitHubClient client) {
			_client = client;
		}

		public async Task<IActionResult> Index() {
			// とりあえずこれを取得
			// https://gist.github.com/ichiroku11/8b7df06784249371c899ef4ea8064b3f
			var gist = await _client.GetGistAsync("8b7df06784249371c899ef4ea8064b3f");

			var file = gist?.Files?.FirstOrDefault();
			if (file == null) {
				return NotFound();
			}

			return Content(file.Value.Value.Content);
		}
	}
}
