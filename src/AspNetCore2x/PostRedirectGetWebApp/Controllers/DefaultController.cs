using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostRedirectGetWebApp.Filters;

namespace PostRedirectGetWebApp.Controllers {
	public class DefaultController : Controller {
		[LoadModelState]
		public IActionResult Index() {
			return new EmptyResult();
		}

		[HttpPost, SaveModelState]
		public IActionResult Index(int value) {
			return new EmptyResult();
		}
	}
}