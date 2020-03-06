using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using Xunit;

namespace PostRedirectGetWebApp.Filters {
	public class ModelStateDictionaryJsonSerializerTest {
		[Fact]
		public void Serialize_RawValueがstringの場合に正しく処理できる() {
			// Arrange
			var modelStates = new ModelStateDictionary();
			modelStates.SetModelValue("k", "r", "a");

			// Act
			var json = ModelStateDictionaryJsonSerializer.Serialize(modelStates);

			// Assert
			Assert.Equal(
				@"[{""key"":""k"",""rawValues"":[""r""],""attemptedValue"":""a"",""errorMessages"":[]}]",
				json);
		}

		[Fact(DisplayName = "Serialize_RawValueがstring[]の場合に正しく処理できる")]
		public void Serialize_RawValueStringArray() {
			// Arrange
			var modelStates = new ModelStateDictionary();
			modelStates.SetModelValue("k", new[] { "r1", "r2" }, "a");

			// Act
			var json = ModelStateDictionaryJsonSerializer.Serialize(modelStates);

			// Assert
			Assert.Equal(
				@"[{""key"":""k"",""rawValues"":[""r1"",""r2""],""attemptedValue"":""a"",""errorMessages"":[]}]",
				json);
		}

		[Fact]
		public void Deserialize_RawValueがstringの場合に正しく処理できる() {
			// Arrange
			var json = @"[{""key"":""k"",""rawValues"":[""r""],""attemptedValue"":""a"",""errorMessages"":[]}]";

			// Act
			var modelStates = ModelStateDictionaryJsonSerializer.Deserialize(json);

			// Assert
			Assert.Single(modelStates);

			var (key, modelState) = modelStates.First();
			Assert.Equal("k", key);

			Assert.IsType<string>(modelState.RawValue);
			Assert.Equal("r", modelState.RawValue);

			Assert.Equal("a", modelState.AttemptedValue);

			Assert.Empty(modelState.Errors);
		}

		[Fact(DisplayName = "Deserialize_RawValueがstring[]の場合に正しく処理できる")]
		public void Deserialize_RawValueStringArray() {
			// Arrange
			var json = @"[{""key"":""k"",""rawValues"":[""r1"",""r2""],""attemptedValue"":""a"",""errorMessages"":[]}]";

			// Act
			var modelStates = ModelStateDictionaryJsonSerializer.Deserialize(json);

			// Assert
			Assert.Single(modelStates);

			var (key, modelState) = modelStates.First();
			Assert.Equal("k", key);

			Assert.IsType<string[]>(modelState.RawValue);
			var rawValues = modelState.RawValue as string[];
			Assert.Equal(2, rawValues.Length);
			Assert.Equal("r1", rawValues[0]);
			Assert.Equal("r2", rawValues[1]);

			Assert.Equal("a", modelState.AttemptedValue);

			Assert.Empty(modelState.Errors);
		}
	}
}
