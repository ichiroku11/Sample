using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public class SendGridGetTemplateListSample : SendGridSample {
		public SendGridGetTemplateListSample(IOptionsMonitor<SendGridOptions> options) : base(options) {
		}

		public override async Task RunAsync(HttpContext context) {
			var client = new SendGridClient(Options.ApiKey);

			var response = await client.RequestAsync(
				method: SendGridClient.Method.GET,
				urlPath: "templates?generations=dynamic");

			await WriteResponseAsync(context, response);
		}
	}
}
