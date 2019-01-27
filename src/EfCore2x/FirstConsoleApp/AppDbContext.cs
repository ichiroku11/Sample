using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FirstConsoleApp {
	// DBコンテキスト
	public class AppDbContext : DbContext {
		public DbSet<Monster> Monsters { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			// 接続文字列を指定する
			var connectionString = new SqlConnectionStringBuilder {
				DataSource = ".",
				InitialCatalog = "Test",
				IntegratedSecurity = true,
			}.ToString();
			optionsBuilder.UseSqlServer(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			// テーブルにマッピングする
			modelBuilder.Entity<Monster>().ToTable("Monster");
		}
	}
}
