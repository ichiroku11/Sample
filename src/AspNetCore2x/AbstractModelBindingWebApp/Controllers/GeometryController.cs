using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbstractModelBindingWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AbstractModelBindingWebApp.Controllers {
	public class GeometryController : Controller {
		public IActionResult Index() {
			return View();
		}

		// POSTされたGeometryTypeの値によって、生成するモデルを切り替える
		private async Task<GeometryModel> CreateModel() {
			var valueProvider = await CompositeValueProvider.CreateAsync(ControllerContext);

			// POSTされたデータの中から"GeometryType"の値を取得
			var valueProviderResult = valueProvider.GetValue(nameof(GeometryModel.GeometryType));

			// 文字列をenumに変換
			if (!Enum.TryParse<GeometryType>(valueProviderResult.FirstValue, true, out var geometryType)) {
				throw new InvalidOperationException();
			}

			// enumからモデルの型を取得
			var modelType = geometryType.GetModelType();

			// モデルを生成
			return (GeometryModel)Activator.CreateInstance(modelType);
		}

		// 保存
		// このメソッドの中では、具象クラスではなく抽象クラスGeometryModelとして扱いたいとする
		[HttpPost]
		public async Task<IActionResult> Save() {
			// モデルを生成
			var model = await CreateModel();

			// モデルにPOSTデータをバインド
			await TryUpdateModelAsync(model, model.GetType(), "");

			// 適当な結果
			return Json(model);
		}

		/*
		// 別解
		[HttpPost]
		public IActionResult Save(GeometryModel model) {
			return Json(model);
		}
		*/
	}
}