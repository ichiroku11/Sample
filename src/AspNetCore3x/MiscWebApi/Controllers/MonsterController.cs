using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiscWebApi.Models;

namespace MiscWebApi.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class MonsterController : ControllerBase {
		private static readonly Dictionary<int, Monster> _monsters
			= new[] {
				new Monster { Id = 1, Name = "スライム" },
				new Monster { Id = 2, Name = "ドラキー" },
			}.ToDictionary(monster => monster.Id);

		[HttpGet]
		public IEnumerable<Monster> Get() => _monsters.Values.OrderBy(monster => monster.Id);
	}
}
