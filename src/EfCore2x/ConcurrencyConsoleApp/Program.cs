using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConcurrencyConsoleApp {
	class Program {
		private static async Task PrepareAsync() {
			// 準備
			using (var dbContext = new AppDbContext()) {
				var sql = @"
drop table if exists dbo.Monster;

create table dbo.Monster(
	Id int not null,
	Name nvarchar(20) not null,
	Hp int not null,
	Ver rowversion not null,
	constraint PK_Monster primary key(Id)
);

insert into dbo.Monster(Id, Name, Hp)
output inserted.*
values
	(1, N'スライム', 7);";
				await dbContext.Database.ExecuteSqlCommandAsync(sql);
			}
		}

		// https://docs.microsoft.com/ja-jp/ef/core/saving/concurrency
		private static async Task TestConcurrencyAsync() {
			// 最初に更新するエンティティ
			var first = default(Monster);
			using (var dbContext = new AppDbContext()) {
				first = await dbContext.Monsters.FirstOrDefaultAsync(monster => monster.Id == 1);
			}

			// 次に更新（正確には更新失敗）するエンティティ
			var second = new Monster {
				Id = first.Id,
				Name = first.Name,
				Hp = 7,
				// 更新前のバージョンを指定
				Ver = first.Ver
			};

			// 1つ目のエンティティの更新 => 成功する
			using (var dbContext = new AppDbContext()) {
				dbContext.Monsters.Attach(first).State = EntityState.Modified;

				await dbContext.SaveChangesAsync();
			}

			// 2つ目のエンティティの更新 => 失敗する
			using (var dbContext = new AppDbContext()) {
				dbContext.Monsters.Attach(second).State = EntityState.Modified;

				try {
					await dbContext.SaveChangesAsync();
				} catch (DbUpdateConcurrencyException excetipn) {
					// 更新に失敗し例外がスローされる
					Console.WriteLine("caught:");
					Console.WriteLine(excetipn);
				}
			}

			/*
			// SQL（ExecuteSqlCommandAsync）で更新
			// 結果が0件になるだけ
			using (var dbContext = new AppDbContext()) {
				var sql = @"
update dbo.Monster
set Hp = @hp
where Id = @id and Ver = @ver;";

				var parameters = new[] {
					new SqlParameter("@hp", second.Hp),
					new SqlParameter("@id", second.Id),
					new SqlParameter("@ver", second.Ver),
				};

				var result = await dbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
				Console.WriteLine($"{nameof(result)}: {result}");
				// result: 0
			}

			// SQL（Dapper）で更新
			// 結果が0件になるだけ
			using (var dbContext = new AppDbContext()) {
				var sql = @"
update dbo.Monster
set Hp = @hp
where Id = @id and Ver = @ver;";

				var parameters = new {
					hp = second.Hp,
					id = second.Id,
					ver = second.Ver,
				};
				var result = await dbContext.Database.GetDbConnection().ExecuteAsync(sql, parameters);
				Console.WriteLine($"{nameof(result)}: {result}");
				// result: 0
			}
			*/
		}

		static async Task Main(string[] args) {
			await PrepareAsync();

			await TestConcurrencyAsync();
		}
	}
}
