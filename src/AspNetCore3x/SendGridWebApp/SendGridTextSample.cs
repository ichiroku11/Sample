using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public class SendGridTextSample : ISendGridSample {
		private readonly SendGridOptions _options;

		public SendGridTextSample(IOptionsMonitor<SendGridOptions> options) {
			_options = options.CurrentValue;
		}

		public async Task RunAsync(HttpContext context) {
			// todo:
			await context.Response.WriteAsync("Done!");
		}
	}
}
