using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilterWebApp.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FilterWebApp.Controllers {
	// コントローラのフィルタを登録
	[TypeFilter(typeof(ControllerActionFilter))]
	public class DefaultController : Controller {
		private readonly ILogger _logger;

		public DefaultController(ILogger<DefaultController> logger) {
			_logger = logger;
		}

		// アクションのフィルタを登録
		[TypeFilter(typeof(ActionActionFilter))]
		public IActionResult Index() {
			_logger.LogInformation(nameof(DefaultController));

			return Content("Hello World!");
		}
		// グローバル、コントローラ、アクションの順に実行される
		/*
		GlobalActionFilter
		ControllerActionFilter
		ActionActionFilter
		DefaultController
		*/
	}
}

