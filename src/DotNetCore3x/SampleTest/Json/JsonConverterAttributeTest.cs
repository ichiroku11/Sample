using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest.Json {
	public class JsonConverterAttributeTest {
		private readonly ITestOutputHelper _output;

		public JsonConverterAttributeTest(ITestOutputHelper output) {
			_output = output;
		}

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

		private class ConverterSample2 {
			public DateTime Value { get; set; }
		}

		private class DateTimeConverter : JsonConverter<DateTime> {
			private static readonly string _format = "yyyy/MM/dd HH:mm:ss";

			public override DateTime Read(
				ref Utf8JsonReader reader,
				Type typeToConvert,
				JsonSerializerOptions options)
				=> DateTime.ParseExact(reader.GetString(), _format, CultureInfo.InvariantCulture);

			public override void Write(
				Utf8JsonWriter writer,
				DateTime value,
				JsonSerializerOptions options)
				=> writer.WriteStringValue(value.ToString(_format, CultureInfo.InvariantCulture));
		}

		[Fact]
		public void デフォルトではDateTimeにデシリアライズできない独自の形式の日付文字列がある() {
			// Arrange
			var json = @"{""value"":""2020/06/01 12:34:56""}";
			var options = new JsonSerializerOptions {
				PropertyNameCaseInsensitive = true,
			};

			// Act
			// Assert
			var exception = Assert.Throws<JsonException>(() => {
				JsonSerializer.Deserialize<ConverterSample2>(json, options);
			});
			_output.WriteLine(exception.ToString()); ;
		}

		[Fact]
		public void 独自の形式の日付文字列をDateTimeにデシリアライズする() {
			// Arrange
			var json = @"{""value"":""2020/06/01 12:34:56""}";
			var options = new JsonSerializerOptions {
				PropertyNameCaseInsensitive = true,
			};
			options.Converters.Add(new DateTimeConverter());

			// Act
			var sample = JsonSerializer.Deserialize<ConverterSample2>(json, options);

			// Assert
			Assert.Equal(new DateTime(2020, 6, 1, 12, 34, 56), sample.Value);
		}
	}
}
