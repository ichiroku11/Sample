using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public class SendGridSendTemplateSample : SendGridSample {
		public SendGridSendTemplateSample(IOptionsMonitor<SendGridOptions> options) : base(options) {
		}

		public async override Task RunAsync(HttpContext context) {
			var client = new SendGridClient(Options.ApiKey);

			var message = new SendGridMessage {
				From = new EmailAddress("test@example.com", nameof(SendGridSendTemplateSample)),
			};
			message.AddTo(new EmailAddress(Options.To));
			message.SetTemplateId(Options.TemplateId);
			message.SetTemplateData(new {
				name = "Taro",
			});

			var response = await client.SendEmailAsync(message);

			await WriteResponseAsync(context, response);
		}
	}
}
