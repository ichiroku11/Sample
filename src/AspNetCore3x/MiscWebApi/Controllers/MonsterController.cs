using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiscWebApi.Models;

namespace MiscWebApi.Controllers {
	// 参考
	// ASP.NET Core を使って Web API を作成する | Microsoft Docs
	// https://docs.microsoft.com/ja-jp/aspnet/core/web-api/?view=aspnetcore-3.1
	// チュートリアル: ASP.NET Core で Web API を作成する | Microsoft Docs
	// https://docs.microsoft.com/ja-jp/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio
	// ASP.NET Core Web API のコントローラー アクションの戻り値の型Controller action return types in ASP.NET Core web API | Microsoft Docs
	// https://docs.microsoft.com/ja-jp/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1

	[Route("api/[controller]")]
	[ApiController]
	public class MonsterController : ControllerBase {
		private static readonly Dictionary<int, Monster> _monsters
			= new[] {
				new Monster { Id = 1, Name = "スライム" },
				new Monster { Id = 2, Name = "ドラキー" },
			}.ToDictionary(monster => monster.Id);


		// 何か時間がかかる処理
		private Task ActionAsync() => Task.CompletedTask;

		// 一覧を取得
		// ~/api/monster
		[HttpGet]
		public async Task<IEnumerable<Monster>> GetAsync() {
			await ActionAsync();
			return _monsters.Values.OrderBy(monster => monster.Id);
		}

		// 単体を取得
		// ~/api/monster/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Monster>> GetByIdAsync(int id) {
			await ActionAsync();

			if (!_monsters.TryGetValue(id, out var monster)) {
				return NotFound();
			}

			return monster;
		}

		// ~/api/monster/form
		[HttpPost("form")]
		public async Task<ActionResult<Monster>> PostFormAsync([FromForm] Monster monster) {
			await ActionAsync();

			return monster;
		}
	}
}
