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

		protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
			return Task.FromResult(AuthenticateResult.NoResult());
			// todo:
			/*
			var header = Request.Headers[HeaderNames.Authorization];
			if (StringValues.IsNullOrEmpty(header)) {
				// todo:
				return Task.FromResult(AuthenticateResult.NoResult());
			}
			*/
		}


		protected override Task HandleChallengeAsync(AuthenticationProperties properties) {
			return base.HandleChallengeAsync(properties);
			// todo:
			/*
			Response.StatusCode = 401;
			Response.Headers.Append(HeaderNames.WwwAuthenticate, "Basic");

			return Task.CompletedTask;
			*/
		}
	}
}
