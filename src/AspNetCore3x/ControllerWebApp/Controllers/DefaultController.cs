using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ControllerWebApp.Controllers {
	public class DefaultController : Controller {
		private readonly ApplicationPartManager _manager;

		public DefaultController(ApplicationPartManager manager) {
			_manager = manager;
		}

		public IActionResult Index() {
			return Content("");
		}
	}
}
