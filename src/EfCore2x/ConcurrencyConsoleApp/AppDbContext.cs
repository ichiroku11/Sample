using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ConcurrencyConsoleApp {
	public class AppDbContext : DbContext {
		private static readonly string _connectionString
			= new SqlConnectionStringBuilder {
				DataSource = ".",
				InitialCatalog = "Test",
				IntegratedSecurity = true,
			}.ToString();

		// todo:
		/*
		// 参考
		// https://docs.microsoft.com/ja-jp/ef/core/miscellaneous/logging
		private static readonly LoggerFactory _factory
			= new LoggerFactory(
				new[] {
					// Ver3.0でObsoleteは消えるらしい
					new ConsoleLoggerProvider((_, level) => level >= LogLevel.Information, true),
				});
		*/

		public DbSet<Monster> Monsters { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder
				// todo:
				/*
				// ロガーファクトリ
				.UseLoggerFactory(_loggerFactory)
				*/
				// デフォルトでトラッキングしようにする
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
				.UseSqlServer(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Monster>().ToTable(nameof(Monster));
		}
	}
}
