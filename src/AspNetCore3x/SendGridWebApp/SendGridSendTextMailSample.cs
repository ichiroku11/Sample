using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public class SendGridSendTextMailSample : ISendGridSample {
		private readonly SendGridOptions _options;

		public SendGridSendTextMailSample(IOptionsMonitor<SendGridOptions> options) {
			_options = options.CurrentValue;
		}

		public async Task RunAsync(HttpContext context) {
			var client = new SendGridClient(_options.ApiKey);

			var message = MailHelper.CreateSingleEmail(
				from: new EmailAddress("test@example.com", "Example User"),
				to: new EmailAddress(_options.To),
				subject: "Hello, SendGrid!",
				plainTextContent: "This mail was sent with SendGrid.",
				htmlContent: null);

			var response = await client.SendEmailAsync(message);
			var responseBody = await response.Body.ReadAsStringAsync();

			await context.Response.WriteAsync($"{nameof(response.StatusCode)}: {response.StatusCode}");
			await context.Response.WriteAsync(Environment.NewLine);
			await context.Response.WriteAsync($"{nameof(response.Body)}: {responseBody}");
		}
	}
}
