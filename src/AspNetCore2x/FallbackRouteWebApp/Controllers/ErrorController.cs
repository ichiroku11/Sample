using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FallbackRouteWebApp.Controllers {
	public class ErrorController : Controller {
		public new IActionResult NotFound() => Content("Not Found");
	}
}
