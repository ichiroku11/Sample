using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TypeScriptWebApp.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class SampleJsonController : ControllerBase {
		public object Get() {
			return new {
				Number = 1,
				Text = "a",
			};
		}
	}
}
