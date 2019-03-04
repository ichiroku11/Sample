using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostRedirectGetWebApp.Filters;
using PostRedirectGetWebApp.Models;

namespace PostRedirectGetWebApp.Controllers {
	public class DefaultController : Controller {
		[LoadModelState]
		public IActionResult Index() {
			return View(new UserFormModel { });
		}

		[HttpPost, SaveModelState]
		public IActionResult Index(UserFormModel model) {
			if (!ModelState.IsValid) {
				return RedirectToAction();
			}

			// 仮
			return Content("保存しました！");
		}
	}
}