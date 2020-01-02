using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth0WebApp {
	public class Auth0Options {
		public string Authority => $"https://{Domain}";
		public string Domain { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
	}
}
