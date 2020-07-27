using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthnWebApp.Controllers {
	public class DefaultController : Controller {
		[AllowAnonymous]
		public IActionResult AllowAnonymous() => Content(nameof(AllowAnonymous));

		public IActionResult RequireAuthenticated() => Content(nameof(RequireAuthenticated));
	}
}
