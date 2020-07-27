using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BasicAuthnWebApp {
	public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions> {
		public BasicAuthenticationHandler(
			IOptionsMonitor<BasicAuthenticationOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock)
			: base(options, logger, encoder, clock) {
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
			// todo:
			throw new NotImplementedException();
		}
	}
}
