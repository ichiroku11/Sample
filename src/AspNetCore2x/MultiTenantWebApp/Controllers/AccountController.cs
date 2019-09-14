using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantWebApp.Models;

namespace MultiTenantWebApp.Controllers {
	public class AccountController : Controller {
		// ログインビュー
		[AllowAnonymous]
		public IActionResult Login() {
			return View();
		}

		// ログインのポスト処理
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(IFormCollection fromValues) {
			// サインインに必要なプリンシパルを作る
			// 本当ならユーザIDとパスワードからユーザを特定して・・・という処理が入るはず
			var claims = new[] {
				// 適当なユーザ名
				new Claim(ClaimTypes.Name, "user01@example.jp"),
				// 適当なテナントID
				new Claim(AppClaimTypes.TenantId, "1"),
			};
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			// サインイン
			await HttpContext.SignInAsync(principal);

			return RedirectToAction("Index", "Default");
		}

		// ログアウト
		[AllowAnonymous]
		public async Task<IActionResult> Logout() {
			// サインアウト
			await HttpContext.SignOutAsync();

			return RedirectToAction("Login");
		}
	}
}
