using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiscWebApi.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class EnumController : ControllerBase {
		public enum Fruit {
			None = 0,
			Apple,
			Banana,
		}

		// ~/api/enum/{fruit}
		[HttpGet("{fruit}")]
		public Fruit Get(Fruit fruit) {
			return fruit;
		}
	}
}
