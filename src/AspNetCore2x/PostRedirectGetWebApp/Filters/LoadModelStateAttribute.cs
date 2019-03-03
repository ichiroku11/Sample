using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PostRedirectGetWebApp.Filters {
	public class LoadModelStateAttribute : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext context) {
			if (!(context.Controller is Controller controller)) {
				// ありえるの？
				return;
			}

			// todo: TempDataから復元する
			var modelState = controller.TempData.GetModelState();
			if (modelState != null) {
				controller.ModelState.Merge(modelState);
			}
		}
	}
}
