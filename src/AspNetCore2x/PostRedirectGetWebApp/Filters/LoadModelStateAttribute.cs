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

			// TempDataからModelStateを取り出す
			var modelState = controller.TempData.GetModelState();
			controller.ModelState.Merge(modelState);
		}
	}
}
