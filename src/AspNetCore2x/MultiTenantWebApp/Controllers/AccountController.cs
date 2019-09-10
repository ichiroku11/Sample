using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MultiTenantWebApp.Controllers {
	public class AccountController : Controller {
		[AllowAnonymous]
		public IActionResult Login() {
			return View();
		}
	}
}
