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
		public IActionResult Index1() {
			_logger.LogInformation(nameof(DefaultController));

			// 各フィルタのOrderは0（デフォルト）なので
			// グローバル、コントローラ、アクションの順に実行される
			// 各フィルタの実行順：
			// 1. GlobalActionFilter
			// 2. ControllerActionFilter
			// 3. ActionActionFilter
			// 4. DefaultControllerの処理

			return Content("Hello World!");
		}

		[TypeFilter(typeof(ActionActionFilter))]
		[TypeFilter(typeof(FirstActionActionFilter), Order = -2)]
		[TypeFilter(typeof(SecondActionActionFilter), Order = -1)]
		public IActionResult Index2() {
			_logger.LogInformation(nameof(DefaultController));

			// Orderの値が小さいフィルタから実行される
			// Orderの値が同じフィルタはスコープ（グローバル、クラス、アクション）順で実行される
			// 各フィルタの実行順：
			// 1. FirstActionActionFilter(Order = -2)
			// 2. SecondActionActionFilter(Order = -1)
			// 3. GlobalActionFilter(Order = 0)
			// 4. ControllerActionFilter(Order = 0)
			// 5. ActionActionFilter(Order = 0)
			// 6. DefaultControllerの処理

			return Content("Hello World!");
		}
	}
}

