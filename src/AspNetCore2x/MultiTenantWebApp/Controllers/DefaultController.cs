using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MultiTenantWebApp.Controllers {
	public class DefaultController : Controller {
		public IActionResult Index() {
			// ログイン済み
			return Content(User.Identity.Name);
		}
	}
}
