using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest.EntityFrameworkCore {
	// 参考
	// https://docs.microsoft.com/ja-jp/ef/core/querying/related-data
	// https://docs.microsoft.com/ja-jp/ef/core/saving/related-data
	public class RelatedDataTest : IDisposable {
		private static class EqualityComparerFactory<TElement> {
			public static EqualityComparer<TElement> Create<TKey>(Func<TElement, TKey> keySelector) {
				return new EqualityComparer<TElement, TKey>(keySelector);
			}
		}

		private class MonsterCategory {
			public int Id { get; set; }
			public string Name { get; set; }
		}

		private class Monster {
			public int Id { get; set; }
			public int CategoryId { get; set; }
			public string Name { get; set; }

			// Navigation
			public MonsterCategory Category { get; set; }
			// todo:
			/*
			public List<MonsterItem> Items { get; set; }
			*/
		}

		// todo:
		/*
		private class Item {
			public int Id { get; set; }
			public string Name { get; set; }
		}

		private class MonsterItem {
			public int MonsterId { get; set; }
			public int ItemId { get; set; }
		}
		*/

		private static readonly IReadOnlyDictionary<int, MonsterCategory> _monsterCategories
			= new Dictionary<int, MonsterCategory>() {
				{ 1, new MonsterCategory { Id = 1, Name = "Slime", } },
				{ 2, new MonsterCategory { Id = 2, Name = "Animal", } },
				{ 3, new MonsterCategory { Id = 3, Name = "Fly", } }
			};

		private static readonly IReadOnlyDictionary<int, Monster> _monsters
			= new Dictionary<int, Monster> {
				{ 1, new Monster { Id = 1, CategoryId = 1, Name = "スライム", } },
				{ 2, new Monster { Id = 2, CategoryId = 2, Name = "ドラキー", } },
			};

		private static readonly EqualityComparer<MonsterCategory> _monsterCategoryComparer
			= EqualityComparerFactory<MonsterCategory>.Create(category => new { category.Id, category.Name });

		private static readonly EqualityComparer<Monster> _monsterComparer
			= EqualityComparerFactory<Monster>.Create(monster => new { monster.Id, monster.CategoryId, monster.Name });

		private class MonsterDbContext : AppDbContext {
			public DbSet<MonsterCategory> MonsterCategories { get; set; }
			public DbSet<Monster> Monsters { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder) {
				modelBuilder.Entity<MonsterCategory>().ToTable(nameof(MonsterCategory));
				modelBuilder.Entity<Monster>().ToTable(nameof(Monster));
			}
		}

		private MonsterDbContext _context;

		public RelatedDataTest() {
			_context = new MonsterDbContext();

			DropTable();
			CreateTable();
		}

		public void Dispose() {
			DropTable();

			if (_context != null) {
				_context.Dispose();
				_context = null;
			}
		}

		private int CreateTable() {
			var sql = @"
create table dbo.MonsterCategory(
	Id int not null,
	Name nvarchar(10) not null,
	constraint PK_MonsterCategory primary key(Id)
);

create table dbo.Monster(
	Id int not null,
	CategoryId int not null,
	Name nvarchar(10) not null,
	constraint PK_Monster primary key(Id),
	constraint FK_Monster_MonsterCategory
		foreign key(CategoryId) references dbo.MonsterCategory(Id)
);";

			return _context.Database.ExecuteSqlRaw(sql);
		}

		private int DropTable() {
			var sql = @"
drop table if exists dbo.Monster;
drop table if exists dbo.MonsterCategory;";

			return _context.Database.ExecuteSqlRaw(sql);
		}

		private Task<int> InitAsync() {
			var sql = @"
delete from dbo.Monster;
delete from dbo.MonsterCategory;";

			return _context.Database.ExecuteSqlRawAsync(sql);
		}

		[Fact]
		public async Task モンスターカテゴリを追加して取得できる() {
			// Arrange
			await InitAsync();

			var expected = _monsterCategories.Values.OrderBy(category => category.Id);

			// Act
			// Assert
			// カテゴリを追加（追加した件数が正しい）
			_context.MonsterCategories.AddRange(expected);
			var rows = await _context.SaveChangesAsync();
			Assert.Equal(expected.Count(), rows);

			// 追加したカテゴリを取得できる
			var actual = await _context.MonsterCategories
				.OrderBy(category => category.Id)
				.ToListAsync();
			Assert.Equal(expected, actual, _monsterCategoryComparer);
		}
	}
}
