
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace BasicAuthWebApp {
	public static class AuthenticationBuilderExtensions {
		public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder)
			=> builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme);

		public static AuthenticationBuilder AddBasic(
			this AuthenticationBuilder builder,
			string authenticationScheme)
			=> builder.AddBasic(authenticationScheme, configureOptions: null);

		public static AuthenticationBuilder AddBasic(
			this AuthenticationBuilder builder,
			Action<BasicAuthenticationOptions> configureOptions)
			=> builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme, configureOptions);

		public static AuthenticationBuilder AddBasic(
			this AuthenticationBuilder builder,
			string authenticationScheme,
			Action<BasicAuthenticationOptions> configureOptions)
			=> builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(
				authenticationScheme, configureOptions);
	}
}
