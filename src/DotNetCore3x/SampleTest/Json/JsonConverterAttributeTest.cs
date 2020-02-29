using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace SampleTest.Json {
	public class JsonConverterAttributeTest {
		private class NumberBooleanConverter : JsonConverter<bool> {
			public override bool Read(
				ref Utf8JsonReader reader,
				Type typeToConvert,
				JsonSerializerOptions options)
				=> reader.GetInt32() > 0;

			public override void Write(
				Utf8JsonWriter writer,
				bool value,
				JsonSerializerOptions options)
				=> writer.WriteNumberValue(value ? 1 : 0);
		}

		private class ConverterSample {
			[JsonConverter(typeof(NumberBooleanConverter))]
			public bool Enable { get; set; }
		}

		[Fact]
		public void JsonConverter属性を使って数値をboolにデシリアライズする() {
			// Arrange
			var json = @"{""enable"":1}";

			var options = new JsonSerializerOptions {
				PropertyNameCaseInsensitive = true,
			};

			// Act
			var data = JsonSerializer.Deserialize<ConverterSample>(json, options);

			// Assert
			Assert.True(data.Enable);
		}
	}
}
