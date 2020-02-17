using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public abstract class SendGridSample {
		public SendGridSample(IOptionsMonitor<SendGridOptions> options) {
			Options = options.CurrentValue;
		}

		protected SendGridOptions Options { get; }

		protected async Task WriteResponseAsync(HttpContext context, Response response) {
			var responseBody = await response.Body.ReadAsStringAsync();

			await context.Response.WriteAsync($"{nameof(response.StatusCode)}: {response.StatusCode}");
			await context.Response.WriteAsync(Environment.NewLine);
			await context.Response.WriteAsync($"{nameof(response.Headers)}:");
			await context.Response.WriteAsync(Environment.NewLine);
			await context.Response.WriteAsync($"{response.Headers}");
			await context.Response.WriteAsync(Environment.NewLine);
			await context.Response.WriteAsync($"{nameof(response.Body)}:");
			await context.Response.WriteAsync(Environment.NewLine);
			await context.Response.WriteAsync($"{responseBody}");
		}

		public abstract Task RunAsync(HttpContext context);
	}
}
