using Microsoft.AspNetCore.Mvc.Testing;
using ModelBindingWebApp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ModelBindingWebApp.Controllers.Test {
	public class GeometryControllerTest : ControllerTestBase {
		public GeometryControllerTest(
			ITestOutputHelper output,
			WebApplicationFactory<Startup> factory)
			: base(output, factory) {
		}

		public static IEnumerable<object[]> GetTestData() {
			yield return new object[] {
				new GeometryLineModel {
					X1 = 1,
					Y1 = 2,
					X2 = 3,
					Y2 = 4,
				},
			};

			yield return new object[] {
				new GeometryCircleModel {
					R = 1,
					X = 2,
					Y = 3,
				},
			};
		}

		private static FormUrlEncodedContent GetContent(GeometryModel geometry) {
			var nameValues = geometry switch  {
				GeometryLineModel line => new Dictionary<string, string> {
					{ nameof(GeometryModel.GeometryType), line.GeometryType.ToString() },
					{ nameof(GeometryLineModel.X1), line.X1.ToString() },
					{ nameof(GeometryLineModel.Y1), line.Y1.ToString() },
					{ nameof(GeometryLineModel.X2), line.X2.ToString() },
					{ nameof(GeometryLineModel.Y2), line.Y2.ToString() },
				},
				GeometryCircleModel circle => new Dictionary<string, string> {
					{ nameof(GeometryModel.GeometryType), circle.GeometryType.ToString() },
					{ nameof(GeometryCircleModel.R), circle.R.ToString() },
					{ nameof(GeometryCircleModel.X), circle.X.ToString() },
					{ nameof(GeometryCircleModel.Y), circle.Y.ToString() },
				},
				_ => throw new ArgumentException(nameof(geometry)),
			};

			return new FormUrlEncodedContent(nameValues);
		}

		[Theory]
		[MemberData(nameof(GetTestData))]
		public async Task Save_サブクラスをバインドできる(GeometryModel model) {
			// Arrange
			using var request = new HttpRequestMessage(HttpMethod.Post, "/geometry/save");
			request.Content = GetContent(model);

			// Act
			using var response = await SendAsync(request);
			var content = await response.Content.ReadAsStringAsync();

			// Assert
			// todo:
			Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
		}
	}
}
