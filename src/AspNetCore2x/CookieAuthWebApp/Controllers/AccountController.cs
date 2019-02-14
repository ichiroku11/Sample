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

namespace CookieAuthWebApp.Controllers {
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
				// 適当なユーザ名を登録しておく
				new Claim(ClaimTypes.Name, "user01@example.jp"),
			};
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			// サインイン
			// 認証クッキーをレスポンスに追加
			await HttpContext.SignInAsync(principal);

			// ログインが必要なアクションにリダイレクト
			return RedirectToAction("Index", "Home");
		}

		// ログアウト
		[AllowAnonymous]
		public async Task<IActionResult> Logout() {
			// サインアウト
			// 認証クッキーをレスポンスから削除
			await HttpContext.SignOutAsync();

			// ログインビューにリダイレクト
			return RedirectToAction("Login");
		}
	}
}