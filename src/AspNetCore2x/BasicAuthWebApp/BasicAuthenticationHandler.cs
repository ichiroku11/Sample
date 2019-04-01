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
		private class HeaderNames {
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

			// todo: AuthenticationProperties
			var context = new BasicValidatePrincipalContext(Context, Scheme, Options, null);
			await Events.ValidatePrincipal(context);

			// todo:
			//AuthenticateResult.Success()

			return AuthenticateResult.NoResult();
		}


		protected override Task HandleChallengeAsync(AuthenticationProperties properties) {
			Response.StatusCode = 401;
			Response.Headers.Append(HeaderNames.WwwAuthenticate, "Basic");

			return Task.CompletedTask;
		}
	}
}
