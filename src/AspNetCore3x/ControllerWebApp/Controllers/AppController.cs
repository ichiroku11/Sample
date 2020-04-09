using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControllerWebApp.Controllers {
	// abstractを指定しないとアクセスできるコントローラになってしまう
	public abstract class AppController : Controller {
		// NoActionを指定しないとActionメソッドになる
		[NonAction]
		public IActionResult Base() => Content(nameof(Base));
	}

}
