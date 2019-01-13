using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AbstractModelBindingWebApp.Controllers {
	public class GeometryController : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}