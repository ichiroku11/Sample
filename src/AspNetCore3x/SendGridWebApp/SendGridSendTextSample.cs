using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public class SendGridSendTextSample : SendGridSample {
		public SendGridSendTextSample(IOptionsMonitor<SendGridOptions> options) : base(options) {
		}

		public override async Task RunAsync(HttpContext context) {
			var client = new SendGridClient(Options.ApiKey);

			var message = MailHelper.CreateSingleEmail(
				from: new EmailAddress("test@example.com", nameof(SendGridSendTextSample)),
				to: new EmailAddress(Options.To),
				subject: "Hello, SendGrid!",
				plainTextContent: "This mail was sent with SendGrid.",
				htmlContent: null);

			var response = await client.SendEmailAsync(message);

			await WriteResponseAsync(context, response);
		}
	}
}
