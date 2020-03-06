using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
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
	}
}
