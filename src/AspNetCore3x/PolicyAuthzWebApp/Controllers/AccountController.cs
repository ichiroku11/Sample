using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolicyAuthzWebApp.Controllers {
	public class AccountController : Controller {
		[AllowAnonymous]
		public IActionResult Login() {
			return Content($"{nameof(Login)}");
		}

		public IActionResult Logout() {
			return Content($"{nameof(Logout)}");
		}
	}
}
