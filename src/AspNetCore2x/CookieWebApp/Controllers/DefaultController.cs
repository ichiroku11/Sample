using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CookieWebApp.Controllers {
	public class DefaultController : Controller {
		public IActionResult Index() {
			return View();
		}

		[HttpPost]
		public IActionResult Add(string key, string value) {
			// クッキーを追加
			Response.Cookies.Append(key, value);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Delete(string key) {
			// クッキーを削除
			Response.Cookies.Delete(key);

			return RedirectToAction("Index");
		}
	}
}
