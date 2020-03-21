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
			public List<MonsterItem> Items { get; set; }
		}
		private class Item {
			public int Id { get; set; }
			public string Name { get; set; }
		}

		private class MonsterItem {
			public int MonsterId { get; set; }
			public int ItemId { get; set; }

			// Navigation
			public Monster Monster { get; set; }
			public Item Item { get; set; }
		}

		private static readonly EqualityComparer<MonsterCategory> _monsterCategoryComparer
			= EqualityComparerFactory<MonsterCategory>.Create(category => new { category.Id, category.Name });

		private static readonly EqualityComparer<Monster> _monsterComparer
			= EqualityComparerFactory<Monster>.Create(monster => new { monster.Id, monster.CategoryId, monster.Name });

		private static readonly EqualityComparer<Item> _itemComparer
			= EqualityComparerFactory<Item>.Create(item => new { item.Id, item.Name });

		private static readonly EqualityComparer<MonsterItem> _monsterItemComparer
			= EqualityComparerFactory<MonsterItem>.Create(item => new { item.MonsterId, item.ItemId });

		private static readonly IReadOnlyDictionary<int, MonsterCategory> _monsterCategories
			= new Dictionary<int, MonsterCategory>() {
				{ 1, new MonsterCategory { Id = 1, Name = "Slime", } },
				{ 2, new MonsterCategory { Id = 2, Name = "Animal", } },
				{ 3, new MonsterCategory { Id = 3, Name = "Fly", } }
			};

		private static readonly IReadOnlyCollection<Monster> _monsters
			= new[] {
				new Monster { Id = 1, CategoryId = 1, Name = "スライム", },
				new Monster { Id = 2, CategoryId = 2, Name = "ドラキー", },
			};

		private static readonly IReadOnlyDictionary<int, Item> _items
			= new Dictionary<int, Item> {
				{ 1, new Item { Id = 1, Name = "やくそう", } },
				{ 2, new Item { Id = 2, Name = "スライムゼリー", } },
				{ 3, new Item { Id = 3, Name = "キメラのつばさ", } },
			};

		private static readonly IReadOnlyCollection<MonsterItem> _monsterItems
			= new[] {
				// スライム => やくそう、スライムゼリー
				new MonsterItem { MonsterId = 1, ItemId = 1, },
				new MonsterItem { MonsterId = 1, ItemId = 2, },
				// ドラキー => やくそう、キメラのつばさ
				new MonsterItem { MonsterId = 2, ItemId = 1, },
				new MonsterItem { MonsterId = 2, ItemId = 3, },
			};

		private class MonsterDbContext : AppDbContext {
			public DbSet<MonsterCategory> MonsterCategories { get; set; }
			public DbSet<Monster> Monsters { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder) {
				modelBuilder.Entity<MonsterCategory>().ToTable(nameof(MonsterCategory));
				modelBuilder.Entity<Monster>().ToTable(nameof(Monster));
				modelBuilder.Entity<Item>().ToTable(nameof(Item));
				modelBuilder.Entity<MonsterItem>().ToTable(nameof(MonsterItem))
					// 複合主キー
					.HasKey(monsterItem => new { monsterItem.MonsterId, monsterItem.ItemId });
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
);

create table dbo.Item(
	Id int not null,
	Name nvarchar(10) not null,
	constraint PK_Item primary key(Id),
);

create table dbo.MonsterItem(
	MonsterId int not null,
	ItemId int not null,
	constraint PK_MonsterItem primary key(MonsterId, ItemId),
	constraint FK_MonsterItem_Monster
		foreign key(MonsterId) references dbo.Monster(Id),
	constraint FK_MonsterItem_Item
		foreign key(ItemId) references dbo.Item(Id)
);";

			return _context.Database.ExecuteSqlRaw(sql);
		}

		private int DropTable() {
			var sql = @"
drop table if exists dbo.MonsterItem;
drop table if exists dbo.Item;
drop table if exists dbo.Monster;
drop table if exists dbo.MonsterCategory;";

			return _context.Database.ExecuteSqlRaw(sql);
		}

		private Task<int> InitAsync() {
			var sql = @"
delete from dbo.MonsterItem;
delete from dbo.Item;
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

		// Includeで1対1の関連データを読み込む
		// - AddRangeでMonsterとMonsterCategoryを同時にinsert
		// - Includeを使わずにMonster一覧を取得
		// - Includeを使ってMonster一覧を取得するときにMonsterCategoryも取得
		[Fact]
		public async Task モンスターを追加する際にカテゴリも追加して取得できる() {
			// Arrange
			await InitAsync();

			var expectedMonsters = _monsters
				.Select(monster => new Monster {
					Id = monster.Id,
					CategoryId = monster.CategoryId,
					Name = monster.Name,
					Category = _monsterCategories[monster.CategoryId]
				})
				.OrderBy(monster => monster.Id);
			var expectedCategories = expectedMonsters
				.Select(monster => monster.Category)
				.Distinct(_monsterCategoryComparer);

			// Act
			// Assert

			// モンスターを追加
			_context.Monsters.AddRange(expectedMonsters);
			var rows = await _context.SaveChangesAsync();

			// カテゴリも追加された数になる
			Assert.Equal(expectedMonsters.Count() + expectedCategories.Count(), rows);

			// 追加されたカテゴリを取得できる
			var actualCategories = await _context.MonsterCategories
				.OrderBy(category => category.Id)
				.ToListAsync();
			Assert.Equal(expectedCategories, actualCategories, _monsterCategoryComparer);

			// 追加したモンスターを取得できる
			var actualMonsters = await _context.Monsters
				.OrderBy(category => category.Id)
				.ToListAsync();
			Assert.Equal(expectedMonsters, actualMonsters, _monsterComparer);
			// Categoryプロパティはすべてnull
			Assert.All(actualMonsters, monster => Assert.Null(monster.Category));

			// モンスターとカテゴリをあわせて取得できる
			actualMonsters = await _context.Monsters
				.Include(monster => monster.Category)
				.OrderBy(category => category.Id)
				.ToListAsync();
			Assert.Equal(expectedMonsters, actualMonsters, _monsterComparer);
			// Categoryプロパティが設定されている
			Assert.All(actualMonsters, monster => Assert.Equal(_monsterCategories[monster.CategoryId], monster.Category, _monsterCategoryComparer));
		}

		// todo:
		/*
		// Includeで1対多の関連データを読み込む
		[Fact]
		public async Task Include_OneMany() {
			// Arrange
			await InitAsync();

			// todo:
			// Itemをinsert
			// MonsterとMonsterItemをinsert

			// Act
			// Assert

			// todo:
			// MonsterとMonsterItemをIncludeで取得

		}

		[Fact]
		public async Task Include_ManyMany() {
			// Arrange
			await InitAsync();

			// Act
			// Assert
		}
		*/
	}
}
