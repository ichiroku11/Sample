using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BasicAuthWebApp {
	public class BasicAuthenticator : IBasicAuthenticator {
		public Task<ClaimsPrincipal> AuthenticateAsync(string userName, string password) {
			// todo:
			var principal = new ClaimsPrincipal();
			return Task.FromResult(principal);
		}
	}
}
