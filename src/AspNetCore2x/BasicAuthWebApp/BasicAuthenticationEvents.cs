using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthWebApp {
	public class BasicAuthenticationEvents {
		public Func<BasicValidatePrincipalContext, Task> OnValidatePrincipal { get; set; }
			= context => Task.CompletedTask;

		public virtual Task ValidatePrincipal(BasicValidatePrincipalContext context) => OnValidatePrincipal(context);
	}
}
