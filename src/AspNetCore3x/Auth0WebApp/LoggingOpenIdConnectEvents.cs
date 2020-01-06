using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth0WebApp {
	public class LoggingOpenIdConnectEvents : OpenIdConnectEvents {
		private ILogger _logger;

		private ILogger GetLogger<TOptions>(BaseContext<TOptions> context) where TOptions : AuthenticationSchemeOptions {
			if (_logger == null) {
				_logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<LoggingOpenIdConnectEvents>>();
			}
			return _logger;
		}

		// Invoked when an access denied error was returned by the remote server.
		public override Task AccessDenied(AccessDeniedContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.AccessDenied(context);
		}

		// Invoked when there is a remote failure.
		public override Task RemoteFailure(RemoteFailureContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.RemoteFailure(context);
		}

		// Invoked after the remote ticket has been received.
		public override Task TicketReceived(TicketReceivedContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.TicketReceived(context);
		}

		public override Task AuthenticationFailed(AuthenticationFailedContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.AuthenticationFailed(context);
		}

		public override Task AuthorizationCodeReceived(AuthorizationCodeReceivedContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.AuthorizationCodeReceived(context);
		}

		public override Task MessageReceived(MessageReceivedContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.MessageReceived(context);
		}
		public override Task RedirectToIdentityProvider(RedirectContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.RedirectToIdentityProvider(context);
		}

		public override Task RedirectToIdentityProviderForSignOut(RedirectContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.RedirectToIdentityProviderForSignOut(context);
		}

		public override Task RemoteSignOut(RemoteSignOutContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.RemoteSignOut(context);
		}

		public override Task SignedOutCallbackRedirect(RemoteSignOutContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.SignedOutCallbackRedirect(context);
		}

		public override Task TokenResponseReceived(TokenResponseReceivedContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.TokenResponseReceived(context);
		}

		public override Task TokenValidated(TokenValidatedContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.TokenValidated(context);
		}

		public override Task UserInformationReceived(UserInformationReceivedContext context) {
			GetLogger(context).LogCallerMethodName();
			return base.UserInformationReceived(context);
		}
	}
}
