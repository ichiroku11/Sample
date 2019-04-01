using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace BasicAuthWebApp {
	// 参考
	// https://github.com/blowdart/idunno.Authentication/
	public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions> {
		private static bool TryExtractCredentials(
			string encodedCredentials,
			out string userName,
			out string password) {
			// todo:

			userName = "x";
			password = "1";

			return true;
		}

		private static class HeaderNames {
			public const string WwwAuthenticate = "WWW-Authenticate";
			public const string Authorization = "Authorization";
		}

		public BasicAuthenticationHandler(
			IOptionsMonitor<BasicAuthenticationOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock)
			: base(options, logger, encoder, clock) {
		}

		protected new BasicAuthenticationEvents Events {
			get => (BasicAuthenticationEvents)base.Events;
			set => base.Events = value;
		}

		protected override Task<object> CreateEventsAsync()
			=> Task.FromResult<object>(new BasicAuthenticationEvents());

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
			var headerValue = (string)Request.Headers[HeaderNames.Authorization];
			if (string.IsNullOrEmpty(headerValue)) {
				// todo:
				return AuthenticateResult.NoResult();
			}

			if (!headerValue.StartsWith("Basic ")) {
				return AuthenticateResult.NoResult();
			}

			var encodedCredentials = headerValue.Substring("Basic ".Length).Trim();
			if (string.IsNullOrEmpty(encodedCredentials)) {
				return AuthenticateResult.Fail("Missing credentials");
			}

			// todo: encodedCredentails => userName, password
			if (!TryExtractCredentials(encodedCredentials, out var userName, out var password)) {
				return AuthenticateResult.Fail("Invalid credentials");
			}

			// todo: AuthenticationProperties
			var context = new BasicValidateCredentialsContext(Context, Scheme, Options, null);
			await Events.ValidateCredentials(context);

			if (context.Principal == null) {
				return AuthenticateResult.Fail("Invalid username or password");
			}

			// todo:
			var ticket = new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
			return AuthenticateResult.Success(ticket);
		}


		protected override Task HandleChallengeAsync(AuthenticationProperties properties) {
			Response.StatusCode = 401;
			Response.Headers.Append(HeaderNames.WwwAuthenticate, "Basic");

			return Task.CompletedTask;
		}
	}
}
