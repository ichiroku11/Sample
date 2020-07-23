using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SampleTest.AspNetCore {
	// 参考
	// https://github.com/dotnet/aspnetcore/blob/master/src/Mvc/Mvc.Core/test/Routing/UrlHelperTest.cs
	// https://github.com/dotnet/aspnetcore/blob/master/src/Mvc/Mvc.Core/test/Routing/UrlHelperTestBase.cs
	public class UrlHelperTest {
		private class PassThroughRouter : IRouter {
			public VirtualPathData GetVirtualPath(VirtualPathContext context) => null;

			public Task RouteAsync(RouteContext routeContext) {
				routeContext.Handler = httpContext => Task.CompletedTask;
				return Task.CompletedTask;
			}
		}

		// IServiceProvider（サービス一覧）を生成
		private static IServiceProvider CreateServiceProvider() {
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

		// HttpContextを生成
		private static HttpContext CreateHttpContext(
			IServiceProvider services,
			string scheme = "http",
			string host = "localhost",
			string app = "") {
			var context = new DefaultHttpContext {
				RequestServices = services
			};

			var request = context.Request;
			request.Scheme = scheme;
			request.Host = new HostString(host);
			request.PathBase = new PathString(app);

			return context;
		}

		// ActionContextを生成
		private static ActionContext CreateActionContext(HttpContext httpContext, RouteData routeData = default)
			=> new ActionContext(httpContext, routeData ?? new RouteData(), new ActionDescriptor());

		// IRouteBuilderを生成
		private static IRouteBuilder CreateRouteBuilder(IServiceProvider services) {
			var app = new Mock<IApplicationBuilder>();

			app.SetupGet(a => a.ApplicationServices)
				.Returns(services);

			return new RouteBuilder(app.Object) {
				DefaultHandler = new PassThroughRouter(),
			};
		}
	}
}
