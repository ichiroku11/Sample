using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthnWebApp {
	public static class AuthenticationBuilderExtensions {
		public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder) {
			return builder.AddBasic(_ => { });
		}

		public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder,
			Action<BasicAuthenticationOptions> configureOptions) {
			// todo:
			throw new NotImplementedException();
		}
	}
}
