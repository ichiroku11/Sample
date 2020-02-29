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
				// 数値をboolとして取得する
				=> reader.GetInt32() > 0;

			public override void Write(
				Utf8JsonWriter writer,
				bool value,
				JsonSerializerOptions options)
				// boolを数値として書き込む
				=> writer.WriteNumberValue(value ? 1 : 0);
		}

		private class ConverterSample {
			[JsonConverter(typeof(NumberBooleanConverter))]
			public bool Enable { get; set; }
		}

		[Fact]
		public void JsonConverter属性を使ってboolを数値にシリアライズする() {
			// Arrange
			var data = new ConverterSample {
				Enable = true,
			};
			var options = new JsonSerializerOptions {
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			};

			// Act
			var json = JsonSerializer.Serialize(data, options);

			// Assert
			Assert.Equal(@"{""enable"":1}", json);
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
