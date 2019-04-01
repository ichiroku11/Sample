using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace BasicAuthWebApp {
	public class BasicValidatePrincipalContext : PrincipalContext<BasicAuthenticationOptions> {
		public BasicValidatePrincipalContext(
			HttpContext context,
			AuthenticationScheme scheme,
			BasicAuthenticationOptions options,
			AuthenticationProperties properties)
			: base(context, scheme, options, properties) {
		}
	}
}
