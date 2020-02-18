using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public class SendGridGetTemplateSample : SendGridSample {
		public SendGridGetTemplateSample(IOptionsMonitor<SendGridOptions> options) : base(options) {
		}

		public override async Task RunAsync(HttpContext context) {
			var templateId = context.Request.RouteValues["id"] as string;

			var client = new SendGridClient(Options.ApiKey);

			var response = await client.RequestAsync(
				method: SendGridClient.Method.GET,
				urlPath: $"templates/{templateId}");

			await WriteResponseAsync(context, response);
		}
	}
}
