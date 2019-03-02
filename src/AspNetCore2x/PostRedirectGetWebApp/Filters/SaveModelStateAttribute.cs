using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PostRedirectGetWebApp.Filters {
	public class SaveModelStateAttribute : ActionFilterAttribute {
		private static bool IsRedirectResult(IActionResult result)
			=> result is RedirectToActionResult || result is RedirectToRouteResult;

		public override void OnActionExecuted(ActionExecutedContext context) {
			// リダイレクトのみが対象
			if (!IsRedirectResult(context.Result)) {
				return;
			}

			var controller = context.Controller as Controller;
			if (controller == null) {
				// ありえるの？
				return;
			}

			// バリデーションに失敗した場合が対象
			if (controller.ModelState.IsValid) {
				return;
			}

			// todo: TempDataに保存する
		}
	}
}
