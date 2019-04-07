using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BasicAuthWebApp {
	public interface IBasicAuthenticator {
		Task<ClaimsPrincipal> AuthenticateAsync(string userName, string password);
	}
}
