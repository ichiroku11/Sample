using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace PostRedirectGetWebApp.Filters {
	public static class JsonConvertHelper {
		// JSONシリアライズ用のオブジェクト
		private class JsonEntry {
			public string Key { get; set; }
			public object RawValue { get; set; }
			public string AttemptedValue { get; set; }
			public IEnumerable<string> ErrorMessages { get; set; }
		}

		// JSONにシリアライズ
		public static string SerializeModelState(ModelStateDictionary modelState) {
			var entries = modelState
				.Select(entry => new JsonEntry {
					Key = entry.Key,
					RawValue = entry.Value.RawValue,
					AttemptedValue = entry.Value.AttemptedValue,
					ErrorMessages = entry.Value.Errors.Select(error => error.ErrorMessage),
				});

			return JsonConvert.SerializeObject(entries);
		}

		// JSONからデシリアライズ
		public static ModelStateDictionary DeerializeModelState(string json) {
			var entries = JsonConvert.DeserializeObject<JsonEntry[]>(json);

			var modelState = new ModelStateDictionary();
			foreach (var entry in entries) {
				modelState.SetModelValue(entry.Key, entry.RawValue, entry.AttemptedValue);
				foreach (var errorMessage in entry.ErrorMessages) {
					modelState.AddModelError(entry.Key, errorMessage);
				}
			}
			return modelState;
		}
	}
}
