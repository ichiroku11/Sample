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
			var gist = await _client.GetGistAsync("990d9fa349c798094b91cd90b185a4b6");

			return Content("HttpClientFactory");
		}
	}
}
