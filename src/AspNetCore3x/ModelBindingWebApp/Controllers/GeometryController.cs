using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelBindingWebApp.Models;

namespace ModelBindingWebApp.Controllers {
	// ポリモーフィズムを使ったモデルバインディングのサンプル
	// https://docs.microsoft.com/ja-jp/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-3.1#polymorphic-model-binding
	public class GeometryController : Controller {
		[HttpPost]
		public IActionResult Save(GeometryModel model) {
			return new EmptyResult();
		}
	}
}
