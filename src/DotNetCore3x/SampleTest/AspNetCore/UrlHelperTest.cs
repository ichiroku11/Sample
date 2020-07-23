using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SampleTest.AspNetCore {
	public class UrlHelperTest {
		// 参考
		// https://github.com/dotnet/aspnetcore/blob/master/src/Mvc/Mvc.Core/test/Routing/UrlHelperTest.cs
		// https://github.com/dotnet/aspnetcore/blob/master/src/Mvc/Mvc.Core/test/Routing/UrlHelperTestBase.cs
		private class PassThroughRouter : IRouter {
			public VirtualPathData GetVirtualPath(VirtualPathContext context) => null;

			public Task RouteAsync(RouteContext routeContext) {
				routeContext.Handler = httpContext => Task.CompletedTask;
				return Task.CompletedTask;
			}
		}

		private static IServiceProvider GetServiceProvider() {
			var services = new ServiceCollection();

			services
				.AddOptions()
				.AddLogging()
				.AddRouting(options => {
					options.LowercaseQueryStrings = true;
					options.LowercaseUrls = true;
				})
				.AddSingleton(UrlEncoder.Default);

			return services.BuildServiceProvider();
		}
	}
}
