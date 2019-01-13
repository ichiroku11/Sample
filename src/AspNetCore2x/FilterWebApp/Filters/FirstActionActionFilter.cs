using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FilterWebApp.Filters {
	// 最初に実行されるアクションフィルタとして設定する
	public class FirstActionActionFilter : IAsyncActionFilter {
		private readonly ILogger _logger;

		public FirstActionActionFilter(ILogger<FirstActionActionFilter> logger) {
			_logger = logger;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			_logger.LogInformation(nameof(FirstActionActionFilter));

			// 次を実行
			await next();
		}
	}
}
