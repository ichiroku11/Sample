using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest.EntityFrameworkCore {
	public class ValueConversionTest {
		private enum MonsterCategory : byte {
			None = 0,
			Slime,
			Animal,
			Fly,
		}

		private class Monster {
			public int Id { get; set; }
			public string Name { get; set; }
			public MonsterCategory Category { get; set; }
		}

		public enum ConvertType {
			// enum <=> tinyint
			Default = 0,
			// enum <=> varchar(10)
			String,
			// enum <=> char(2)
			Char2,
		}

		private class MonsterDbContext : AppDbContext {
			public DbSet<Monster> Monsters { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder) {
				modelBuilder.Entity<Monster>().ToTable(nameof(Monster));
			}
		}

		private class EnumStringMonsterDbContext : MonsterDbContext {
			protected override void OnModelCreating(ModelBuilder modelBuilder) {
				modelBuilder.Entity<Monster>().ToTable(nameof(Monster))
					.Property(entity => entity.Category)
					.HasConversion<string>();
			}
		}

		private class EnumChar2MonsterDbContext : MonsterDbContext {
			protected override void OnModelCreating(ModelBuilder modelBuilder) {
				// todo:
				modelBuilder.Entity<Monster>().ToTable(nameof(Monster))
					.Property(entity => entity.Category)
					.HasConversion<string>();
			}
		}

		private static async Task InitAsync(MonsterDbContext context) {
			{
				var sql = @"drop table if exists dbo.Monster;";
				await context.Database.ExecuteSqlRawAsync(sql);
			}
			{
				var sql = context switch {
					EnumStringMonsterDbContext _ => @"
create table dbo.Monster(
	Id int,
	Name nvarchar(20),
	Category varchar(10),
	constraint PK_Monster primary key(Id)
);",
					EnumChar2MonsterDbContext _ => @"
create table dbo.Monster(
	Id int,
	Name nvarchar(20),
	Category char(2),
	constraint PK_Monster primary key(Id)
);",
					_ => @"
create table dbo.Monster(
	Id int,
	Name nvarchar(20),
	Category tinyint,
	constraint PK_Monster primary key(Id)
);",
				};
				await context.Database.ExecuteSqlRawAsync(sql);
			}
		}

		private static async Task AddAsync(MonsterDbContext context, params Monster[] monsters) {
			await context.Monsters.AddRangeAsync(monsters);
			await context.SaveChangesAsync();
		}

		private static async Task<Monster> FindAsync(MonsterDbContext context, int id) {
			return await context.Monsters.FirstOrDefaultAsync(monster => monster.Id == id);
		}

		[Theory]
		[InlineData(ConvertType.Default)]
		[InlineData(ConvertType.String)]
		public async Task Enumは数値や文字列に変換できるAsync(ConvertType convertType) {
			using var context = convertType switch
			{
				ConvertType.String => new EnumStringMonsterDbContext(),
				ConvertType.Char2 => new EnumChar2MonsterDbContext(),
				_ => new MonsterDbContext(),
			};

			try {
				await InitAsync(context);

				var expected = new Monster {
					Id = 1,
					Name = "スライム",
					Category = MonsterCategory.Slime,
				};
				await AddAsync(context, expected);

				var actual = await FindAsync(context, 1);

				Assert.Equal(expected.Id, actual.Id);
				Assert.Equal(expected.Name, actual.Name);
				Assert.Equal(expected.Category, actual.Category);
			} catch (Exception) {
				AssertHelper.Fail();
			}
		}
	}
}
