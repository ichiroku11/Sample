using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FilterWebApp.Filters {
	// アクションメソッドで指定するフィルタ
	public class ActionActionFilter : IAsyncActionFilter {
		private readonly ILogger _logger;

		public ActionActionFilter(ILogger<ActionActionFilter> logger) {
			_logger = logger;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			_logger.LogInformation(nameof(ActionActionFilter));

			// 次を実行
			await next();
		}
	}
}
