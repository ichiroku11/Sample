using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthWebApp {
	public class BasicAuthenticationEvents {
		// todo: Validateじゃない気がする
		public Func<BasicValidateCredentialsContext, Task> OnValidateCredentials { get; set; }
			= context => Task.CompletedTask;

		public virtual Task ValidateCredentials(BasicValidateCredentialsContext context) => OnValidateCredentials(context);
	}
}
