using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PostRedirectGetWebApp.Filters {
	public static class TempDataDictionaryExtensions {
		private const string _modelStateKey = ".modelState";

		public static void AddModelState(this ITempDataDictionary tempData, ModelStateDictionary modelState) {
			var json = JsonConvertHelper.SerializeModelState(modelState);

			tempData.Add(_modelStateKey, json);
		}

		public static ModelStateDictionary GetModelState(this ITempDataDictionary tempData) {
			// todo: いる？
			if (!tempData.ContainsKey(_modelStateKey)) {
				return null;
			}

			var json = tempData[_modelStateKey] as string;
			// todo: ↓でもいい？
			/*
			if(string.IsNullOrWhiteSpace(json)) {
				return null;
			}
			*/

			return JsonConvertHelper.DeerializeModelState(json);
		}

	}
}
