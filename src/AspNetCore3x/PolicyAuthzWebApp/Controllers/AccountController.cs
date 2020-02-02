using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolicyAuthzWebApp.Controllers {
	public class AccountController : Controller {
		// LoginのPOSTのイメージで
		[AllowAnonymous]
		public IActionResult Login() {
			// プリンシパルを作成
			// authenticationTypeはこれで良いのか自信なし
			var authenticationType = CookieAuthenticationDefaults.AuthenticationScheme;
			var claims = new[] {
				new Claim(ClaimTypes.NameIdentifier, "1")
			};
			var identity = new ClaimsIdentity(claims, authenticationType);
			var principal = new ClaimsPrincipal(identity);

			// サインイン
			return SignIn(principal, CookieAuthenticationDefaults.AuthenticationScheme);
		}

		public IActionResult Logout() {
			// サインアウト
			return SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
