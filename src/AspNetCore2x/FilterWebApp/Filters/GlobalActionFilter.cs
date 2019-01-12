using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FilterWebApp.Filters {
	// グローバルフィルタとして設定する
	public class GlobalActionFilter : IAsyncActionFilter {
		private readonly ILogger _logger;

		public GlobalActionFilter(ILogger<GlobalActionFilter> logger) {
			_logger = logger;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			_logger.LogInformation(nameof(GlobalActionFilter));

			// 次を実行
			await next();
		}
	}
}
