using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LoggingConsoleApp {
	class Program {
		static void Main(string[] args) {
			// 準備
			using (var dbContext = new AppDbContext()) {
				var sql = @"
drop table if exists dbo.Monster;

create table dbo.Monster(
	Id int,
	Name nvarchar(20),
	constraint PK_Monster primary key(Id)
);

insert into dbo.Monster(Id, Name)
output inserted.*
values
	(1, N'スライム'),
	(2, N'ドラキー');";
				dbContext.Database.ExecuteSqlRaw(sql);
			}

			using (var dbContext = new AppDbContext()) {
				// データを取得
				var monsters = dbContext.Monsters.ToList();
				foreach (var monster in monsters) {
					Console.WriteLine($"#{monster.Id} {monster.Name}");
				}
				//#1 スライム
				//#2 ドラキー

				/*
				var monsters = dbContext.Monsters.FromSqlRaw("select * from dbo.Monster;");
				foreach (var monster in monsters) {
					Console.WriteLine($"#{monster.Id} {monster.Name}");
				}
				*/
			}
		}
	}
}
