using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PostRedirectGetWebApp.Filters {
	public static class ModelStateDictionarySerializer {
		// JSONシリアライズ用のオブジェクト
		private class JsonEntry {
			public string Key { get; set; }
			// todo: コンバータを試す
			public object RawValue { get; set; }
			public string AttemptedValue { get; set; }
			public IEnumerable<string> ErrorMessages { get; set; }
		}

		// JSON文字列にシリアライズ
		public static string Serialize(ModelStateDictionary modelStates) {
			var entries = modelStates.Select(entry => new JsonEntry {
				Key = entry.Key,
				RawValue = entry.Value.RawValue,
				AttemptedValue = entry.Value.AttemptedValue,
				ErrorMessages = entry.Value.Errors.Select(error => error.ErrorMessage),
			});

			return JsonSerializer.Serialize(entries);
		}

		// JSON文字列をデシリアライズ
		public static ModelStateDictionary Deserialize(string json) {
			var modelStates = new ModelStateDictionary();

			var entries = JsonSerializer.Deserialize<JsonEntry[]>(json);
			foreach (var entry in entries) {
				// todo: rawValueが微妙
				modelStates.SetModelValue(entry.Key, entry.RawValue, entry.AttemptedValue);
				foreach (var errorMessage in entry.ErrorMessages) {
					modelStates.AddModelError(entry.Key, errorMessage);
				}
			}

			return modelStates;
		}
	}
}
