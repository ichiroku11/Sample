using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

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
				foreach (var action in controller.DeclaredMethods) {
					content.AppendLine($"{controller.Name}, {action.Name}");
				}
			}

			return Content(content.ToString());
		}
	}
}
