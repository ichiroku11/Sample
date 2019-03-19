using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SessionWebApp.Controllers {
	public class DefaultController : Controller {
		private ISession Session => HttpContext.Session;


		public IActionResult Index() {
			return View();
		}

		[HttpPost]
		public IActionResult Set(string key, string value) {
			// セッションに設定
			Session.SetString(key, value);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Delete(string key) {
			// セッションから削除
			Session.Remove(key);

			return RedirectToAction("Index");
		}
	}
}
