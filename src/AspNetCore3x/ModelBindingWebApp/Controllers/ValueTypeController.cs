using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ModelBindingWebApp.Controllers {
	// 値型へのバインドを試すサンプル
	public class ValueTypeController : Controller {
		// クエリ文字列の場合
		[HttpGet]
		public IActionResult GetWithQuery(int value) => Content($"{value}");

		// ルートパラメータの場合
		[HttpGet("[controller]/[action]/{value}")]
		public IActionResult GetWithRoute(int value) => Content($"{value}");

		// POSTした場合
		public IActionResult Post(int value) => Content($"{value}");
	}

}
