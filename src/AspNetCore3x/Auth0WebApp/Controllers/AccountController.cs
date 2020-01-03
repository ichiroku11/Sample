using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Auth0WebApp.Controllers {
	public class AccountController : Controller {
		public IActionResult Login() {
			return Challenge(
				new AuthenticationProperties {
					RedirectUri = "/",
				},
				"Auth0");
		}

		public IActionResult Logout() {
			return SignOut(
				"Auth0",
				CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
