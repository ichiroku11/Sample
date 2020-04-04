using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest.EntityFrameworkCore {
	public class DbSetSubQueryTest : IDisposable {
		private class MenuItem {
			public int Id { get; set; }
			public string Name { get; set; }
			public decimal Price { get; set; }
		}

		private class SampleDbContext : AppDbContext {
			public DbSet<MenuItem> MenuItems { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder) {
				modelBuilder.Entity<MenuItem>().ToTable(nameof(MenuItem));
			}
		}

		private SampleDbContext _context;

		public DbSetSubQueryTest() {
			_context = new SampleDbContext();

			DropTable();
			InitTable();
		}

		public void Dispose() {
			DropTable();

			if (_context != null) {
				_context.Dispose();
				_context = null;
			}
		}

		private void InitTable() {
			var sql = @"
create table dbo.MenuItem(
	Id int not null,
	Name nvarchar(5) not null,
	Price decimal(5) not null,
	constraint PK_MenuItem primary key(Id)
);

insert into dbo.MenuItem(Id, Name, Price)
output inserted.*
values
	(1, N'けい', 500),
	(2, N'かわ', 300),
	(3, N'ねぎま', 300),
	(4, N'しろ', 400);";
			_context.Database.ExecuteSqlRaw(sql);
		}

		private void DropTable() {
			var sql = @"drop table if exists dbo.MenuItem;";
			_context.Database.ExecuteSqlRaw(sql);
		}

		[Fact]
		public async Task サブクエリで平均Price以上のMenuItemを取得() {
			var items = await _context.MenuItems
				// 平均Price以上
				.Where(item => item.Price >= _context.MenuItems.Average(item => item.Price))
				.ToListAsync();
			// 実行されるクエリ
			/*
			SELECT [m].[Id], [m].[Name], [m].[Price]
			FROM [MenuItem] AS [m]
			WHERE [m].[Price] >= (
				SELECT AVG([m0].[Price])
				FROM [MenuItem] AS [m0])
			*/

			Assert.Equal(2, items.Count());
			Assert.Contains(items, item => string.Equals(item.Name, "けい"));
			Assert.Contains(items, item => string.Equals(item.Name, "しろ"));
		}
	}
}
