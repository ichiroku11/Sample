using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostRedirectGetWebApp.Filters {
	// ModelStateDictionaryのJsonSerializer
	public static class ModelStateDictionaryJsonSerializer {
		// JSONシリアライズ用のオブジェクト
		private class JsonEntry {
			public string Key { get; set; }
			// ModelStateEntry.RawValueはおそらくstringかstring[]になる
			// JSONでstringとstring[]の2パターンの読み込みが難しそうなのでstring[]として扱う
			public string[] RawValues { get; set; }
			public string AttemptedValue { get; set; }
			public IEnumerable<string> ErrorMessages { get; set; }
		}

		private static JsonSerializerOptions _jsonSerializerOptions
			= new JsonSerializerOptions {
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			};

		// JSON文字列にシリアライズ
		public static string Serialize(ModelStateDictionary modelStates) {
			var entries = modelStates.Select(entry => new JsonEntry {
				Key = entry.Key,
				RawValues = entry.Value.RawValue switch {
					null => new string[0],
					string rawValue => new[] { rawValue },
					string[] rawValues => rawValues,
					_ => throw new InvalidOperationException(),
				},
				AttemptedValue = entry.Value.AttemptedValue,
				ErrorMessages = entry.Value.Errors.Select(error => error.ErrorMessage),
			});

			return JsonSerializer.Serialize(entries, _jsonSerializerOptions);
		}

		// JSON文字列をデシリアライズ
		public static ModelStateDictionary Deserialize(string json) {
			var modelStates = new ModelStateDictionary();

			var entries = JsonSerializer.Deserialize<JsonEntry[]>(json, _jsonSerializerOptions);
			foreach (var entry in entries) {
				var rawValue = entry.RawValues.Length switch {
					0 => (object)null,
					1 => entry.RawValues[0],
					_ => entry.RawValues,
				};
				modelStates.SetModelValue(entry.Key, rawValue, entry.AttemptedValue);
				foreach (var errorMessage in entry.ErrorMessages) {
					modelStates.AddModelError(entry.Key, errorMessage);
				}
			}

			return modelStates;
		}
	}
}
