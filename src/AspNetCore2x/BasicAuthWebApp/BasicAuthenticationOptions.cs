using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace BasicAuthWebApp {
	public class BasicAuthenticationOptions : AuthenticationSchemeOptions {
		public BasicAuthenticationOptions() {
			Events = new BasicAuthenticationEvents();
		}

		public new BasicAuthenticationEvents Events {
			get => (BasicAuthenticationEvents)base.Events;
			set => base.Events = value;
		}
	}
}
