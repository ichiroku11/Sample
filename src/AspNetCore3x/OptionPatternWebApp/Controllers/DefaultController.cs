using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionPatternWebApp.Controllers {
	public class DefaultController : Controller {
		private readonly SampleOptions _options;

		public DefaultController(IOptions<SampleOptions> options) {
			_options = options.Value;
		}

		public IActionResult Index() {
			var content
				= $"{nameof(SampleOptions.Value1)}: {_options.Value1}"
				+ Environment.NewLine
				+ $"{nameof(SampleOptions.Value2)}: {_options.Value2}";
			return Content(content);
		}
	}
}
