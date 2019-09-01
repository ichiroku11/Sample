using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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

		// 参考
		// https://docs.microsoft.com/ja-jp/ef/core/miscellaneous/logging
		private static readonly LoggerFactory _loggerFactory
			= new LoggerFactory(
				new[] {
					// Information以上のログを残す
					// EF Core 3.0でObsoleteは消えるらしい
					new ConsoleLoggerProvider((_, level) => level >= LogLevel.Information, true),
				});

		public DbSet<Monster> Monsters { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder
				// ロガーファクトリ
				.UseLoggerFactory(_loggerFactory)
				// SQL Server
				.UseSqlServer(_connectionString)
				// デフォルトでトラッキングしない
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Monster>().ToTable(nameof(Monster));
		}
	}
}
