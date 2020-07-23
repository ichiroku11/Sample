using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Xunit;

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

		// IRouterを生成
		private static IRouter CreateRouter(IServiceProvider services) {
			var routeBuilder = CreateRouteBuilder(services);
			// デフォルトのルートを追加
			routeBuilder.MapRoute(
				name: "default",
				template: "{controller}/{action}/{id?}",
				new { controller = "default", action = "index" });
			return routeBuilder.Build();
		}

		[Fact]
		public void ActionLink_絶対URLを生成できる() {
			// Arrange
			var services = CreateServiceProvider();
			var httpContext = CreateHttpContext(services);
			var actionContext = CreateActionContext(httpContext);
			actionContext.RouteData.Routers.Add(CreateRouter(services));
			var urlHelper = new UrlHelper(actionContext);

			// Act
			var url = urlHelper.ActionLink();

			// Assert
			Assert.Equal("http://localhost/", url);
		}
	}
}
