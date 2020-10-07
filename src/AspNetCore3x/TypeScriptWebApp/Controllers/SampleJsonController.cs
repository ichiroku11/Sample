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
		public class Sample {
			public int Number { get; set; }
			public string Text { get; set; }
		}

		public object Get() {
			return new Sample {
				Number = 1,
				Text = "a",
			};
		}

		[HttpPost]
		public object Post(Sample sample) {
			return sample;
		}

		[HttpPost("form")]
		public object PostForm([FromForm]Sample sample) {
			return sample;
		}
	}
}
