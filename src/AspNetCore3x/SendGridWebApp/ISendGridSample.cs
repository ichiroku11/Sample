using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridWebApp {
	public interface ISendGridSample {
		Task RunAsync(HttpContext context);
	}
}
