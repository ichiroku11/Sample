using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiscWebApi.Models;

namespace MiscWebApi.Controllers {
	// ディクショナリへのバインドを試す
	// https://docs.microsoft.com/ja-jp/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1

	[Route("api/[controller]")]
	[ApiController]
	public class DictionaryController : ControllerBase {
		// 何か時間がかかる処理
		private Task ActionAsync() => Task.CompletedTask;

		// ~/api/dictionary
		[HttpPost]
		public async Task<IDictionary<string, int>> PostAsync(
			// Formデータをバインドする
			[FromForm] IDictionary<string, int> values) {
			await ActionAsync();

			return values;
		}

		// todo: バインドできない？
		// ~/api/dictionary/complex
		[HttpPost("complex")]
		public async Task<IDictionary<int, Sample>> PostAsync(
			[FromForm] IDictionary<int, Sample> values) {
			await ActionAsync();

			return values;
		}
	}
}
