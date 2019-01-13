using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FilterWebApp.Filters {
	// 2つ目に実行されるアクションフィルタとして設定する
	public class SecondActionActionFilter : IAsyncActionFilter {
		private readonly ILogger _logger;

		public SecondActionActionFilter(ILogger<SecondActionActionFilter> logger) {
			_logger = logger;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
			_logger.LogInformation(nameof(SecondActionActionFilter));

			// 次を実行
			await next();
		}
	}
}
