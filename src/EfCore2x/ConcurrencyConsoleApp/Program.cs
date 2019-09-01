using Microsoft.EntityFrameworkCore;
using System;
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

		static async Task Main(string[] args) {
			await PrepareAsync();
		}
	}
}
