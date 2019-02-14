using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthWebApp.Controllers {
	public class HomeController : Controller {
		public IActionResult Index() {
			// ユーザ名を表示する
			// User.Identity.Nameは"user01@example.jp"
			return Content(User.Identity.Name);
		}
	}
}
