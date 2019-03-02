using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PostRedirectGetWebApp.Filters {
	public class LoadModelStateAttribute : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext context) {
			var controller = context.Controller as Controller;
			if (controller == null) {
				// ありえるの？
				return;
			}

			// todo: TempDataから復元する
		}
	}
}
