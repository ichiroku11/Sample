using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ControllerWebApp.Controllers {
	public class DefaultController : AppController {
		private readonly ApplicationPartManager _manager;

		public DefaultController(ApplicationPartManager manager) {
			_manager = manager;
		}

		// privateなメソッドはActionメソッドにならない
		private string PrivateMethod() => nameof(PrivateMethod);
		// privateなGetterプロパティはActionメソッドにならない
		private string PrivateGetterProperty => nameof(PrivateGetterProperty);

		// publicなメソッドはActionメソッドになる
		public string PublicMethod() => nameof(PublicMethod);
		// publicなGetterプロパティはActionメソッドにならない
		public string PublicGetterProperty => nameof(PublicGetterProperty);

		public IActionResult Index() => Content($"Default.{nameof(Index)}");

		[NonAction]
		public IActionResult NonAction() => Content($"Default.{nameof(NonAction)}");

		[HttpGet]
		public IActionResult HttpGet() => Content($"Default.{nameof(HttpGet)}");
		[HttpPost]
		public IActionResult HttpPost() => Content($"Default.{nameof(HttpPost)}");

		public IActionResult Controllers() {
			// コントローラ一覧を取得
			var feature = new ControllerFeature();
			_manager.PopulateFeature(feature);

			var content = new StringBuilder();
			foreach (var controller in feature.Controllers) {
				content.AppendLine(controller.Name);
			}

			return Content(content.ToString());
		}

		public IActionResult ControllerActions() {
			// コントローラ一覧を取得
			var feature = new ControllerFeature();
			_manager.PopulateFeature(feature);

			var content = new StringBuilder();
			// アクション一覧
			foreach (var controller in feature.Controllers) {
				var controllerArea = controller.GetAttribute<AreaAttribute>(true)?.RouteValue;
				foreach (var action in controller.DeclaredMethods) {
					var nonAction = action.GetAttribute<NonActionAttribute>(false);
					if (nonAction != null) {
						continue;
					}

					var actionArea = action.GetAttribute<AreaAttribute>(false)?.RouteValue;

					var httpMethods = action.GetAttribute<HttpMethodAttribute>(false)?.HttpMethods ?? Enumerable.Empty<string>();

					content.AppendLine($"{controllerArea ?? actionArea}, {controller.Name}, {action.Name}, {string.Join("/", httpMethods)}");
				}
			}

			return Content(content.ToString());
		}
	}
}
