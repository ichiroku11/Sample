using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace BasicAuthWebApp {
	// todo: Validateじゃない気もする
	// todo: PrincipalContextを使う？
	public class BasicValidateCredentialsContext : PrincipalContext<BasicAuthenticationOptions> {
		public BasicValidateCredentialsContext(
			HttpContext context,
			AuthenticationScheme scheme,
			BasicAuthenticationOptions options,
			AuthenticationProperties properties)
			: base(context, scheme, options, properties) {
		}
	}
}
