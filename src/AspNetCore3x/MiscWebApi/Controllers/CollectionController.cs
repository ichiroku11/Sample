using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiscWebApi.Controllers {
	// コレクションへのバインドを試す
	// https://docs.microsoft.com/ja-jp/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1

	[Route("api/[controller]")]
	[ApiController]
	public class CollectionController : ControllerBase {
		// 何か時間がかかる処理
		private Task ActionAsync() => Task.CompletedTask;

		// ~/api/collection
		[HttpPost]
		public async Task<IEnumerable<int>> PostAsync(
			// Formデータをバインドする
			[FromForm] IEnumerable<int> values) {
			await ActionAsync();

			return values;
		}

		// todo: 単純型のコレクション
		// todo: 複合型のコレクション
	}
}
