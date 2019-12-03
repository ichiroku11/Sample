using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace LoggingConsoleApp {
	public class Monster {
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class AppDbContext : DbContext {
		private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => {
			builder
				// フィルタをするなら
				.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
				.AddConsole();
		});

		public AppDbContext() {
			// デフォルトでトラッキングしない
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public DbSet<Monster> Monsters { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			// ロガーファクトリを登録する
			// コンテキストインスタンスごとに新しいILoggerFactoryインスタンスを作成しないこと
			optionsBuilder.UseLoggerFactory(_loggerFactory);

			// SQL Serverの接続文字列を指定する
			var connectionString = new SqlConnectionStringBuilder {
				DataSource = ".",
				InitialCatalog = "Test",
				IntegratedSecurity = true,
			}.ToString();
			optionsBuilder.UseSqlServer(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			// テーブルにマッピングする
			modelBuilder.Entity<Monster>().ToTable(nameof(Monster));
		}
	}

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
