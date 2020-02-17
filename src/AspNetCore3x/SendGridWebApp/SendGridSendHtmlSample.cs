using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public class SendGridSendHtmlSample : SendGridSample {
		public SendGridSendHtmlSample(IOptionsMonitor<SendGridOptions> options) : base(options) {
		}

		public override async Task RunAsync(HttpContext context) {
			var client = new SendGridClient(Options.ApiKey);

			var message = MailHelper.CreateSingleEmail(
				from: new EmailAddress("test@example.com", nameof(SendGridSendHtmlSample)),
				to: new EmailAddress(Options.To),
				subject: "Hello, SendGrid!",
				plainTextContent: null,
				htmlContent: "<p>This mail was sent with <strong>SendGrid</strong>.</p>");

			var response = await client.SendEmailAsync(message);
			await WriteResponseAsync(context, response);
		}
	}
}
