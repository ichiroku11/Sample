using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PostRedirectGetWebApp.Filters {
	public static class TempDataDictionaryExtensions {
		private const string _modelStateKey = ".modelState";

		// ModelStateを追加
		public static void AddModelState(this ITempDataDictionary tempData, ModelStateDictionary modelState) {
			var json = JsonConvertHelper.SerializeModelState(modelState);

			tempData.Add(_modelStateKey, json);
		}

		// ModelStateを取得
		public static ModelStateDictionary GetModelState(this ITempDataDictionary tempData) {
			var json = tempData[_modelStateKey] as string;
			if (string.IsNullOrWhiteSpace(json)) {
				return null;
			}

			return JsonConvertHelper.DeerializeModelState(json);
		}

	}
}
