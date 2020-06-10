using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MiscWebApi.Controllers.Test {
	public class DictionaryControllerTest : ControllerTestBase {
		public DictionaryControllerTest(ITestOutputHelper output, WebApplicationFactory<Startup> factory)
			: base(output, factory) {
		}

		public static IEnumerable<object[]> GetSimpleValues() {
			// values[a]=1&values[b]=2
			yield return new object[] {
				new Dictionary<string, string>() {
					{ "values[a]", "1" },
					{ "values[b]", "2" },
				},
			};

			// [a]=1&[b]=2
			yield return new object[] {
				new Dictionary<string, string>() {
					{ "[a]", "1" },
					{ "[b]", "2" },
				},
			};

			yield return new object[] {
				new Dictionary<string, string>() {
					{ "values[0].Key", "a" },
					{ "values[0].Value", "1" },
					{ "values[1].Key", "b" },
					{ "values[1].Value", "2" },
				},
			};

			yield return new object[] {
				new Dictionary<string, string>() {
					{ "[0].Key", "a" },
					{ "[0].Value", "1" },
					{ "[1].Key", "b" },
					{ "[1].Value", "2" },
				},
			};
		}

		[Theory(DisplayName = "IDictionary<int, string>型のvaluesにバインドできる")]
		[MemberData(nameof(GetSimpleValues))]
		public async Task PostAsync_BindToInt32StringDictionary(IEnumerable<KeyValuePair<string, string>> formValues) {
			// Arrange
			var request = new HttpRequestMessage(HttpMethod.Post, "api/dictionary") {
				Content = new FormUrlEncodedContent(formValues),
			};

			// Act
			var response = await SendAsync(request);
			var values = await DeserializeAsync<IDictionary<string, int>>(response);

			// Assert
			Assert.Equal(2, values.Count);
			Assert.Equal(1, values["a"]);
			Assert.Equal(2, values["b"]);
		}
	}
}
