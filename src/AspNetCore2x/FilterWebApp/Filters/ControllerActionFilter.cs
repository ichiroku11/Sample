using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FilterWebApp.Filters {
	// コントローラで指定するフィルタ
	public class ControllerActionFilter : IAsyncActionFilter {
		private readonly ILogger _logger;

		public ControllerActionFilter(ILogger<ControllerActionFilter> logger) {
			_logger = logger;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			_logger.LogInformation(nameof(ControllerActionFilter));

			// 次を実行
			await next();
		}
	}
}
