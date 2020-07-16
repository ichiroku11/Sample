using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ModelBindingWebApp.Controllers {
	public class ValueTypeController : Controller {
		[HttpGet]
		public IActionResult GetWithQuery(int value) => Content($"{value}");

		[HttpGet("[controller]/[action]/{value}")]
		public IActionResult GetWithRoute(int value) => Content($"{value}");

		public IActionResult Post(int value) => Content($"{value}");
	}

}
