using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareWebApp {
	public class SampleStartupFilter : IStartupFilter {
		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) {
			// todo:
			throw new NotImplementedException();
		}
	}
}
