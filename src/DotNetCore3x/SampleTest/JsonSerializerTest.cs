using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest {
	public class JsonSerializerTest {
		private readonly ITestOutputHelper _output;

		public JsonSerializerTest(ITestOutputHelper output) {
			_output = output;
		}

		[Fact]
		public void Serialize_PropertyNamingPolicyを使ってプロパティ名をキャメルケースで出力する() {
			// Arrange
			var model = new { Number = 1, Text = "Abc" };
			var options = new JsonSerializerOptions {
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			};

			// Act
			var actual = JsonSerializer.Serialize(model, options);
			_output.WriteLine(actual);

			// Assert
			var expected = @"{""number"":1,""text"":""Abc""}";
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Serialize_整形して出力する() {
			// Arrange
			var model = new { Number = 1, Text = "Abc" };
			var options = new JsonSerializerOptions {
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = true
			};

			// Act
			var actual = JsonSerializer.Serialize(model, options);
			_output.WriteLine(actual);

			// Assert
			var expected = @"{
  ""number"": 1,
  ""text"": ""Abc""
}";
			Assert.Equal(expected, actual);
		}
	}
}
