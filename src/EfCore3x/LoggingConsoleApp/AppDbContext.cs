using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingConsoleApp {
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
}
