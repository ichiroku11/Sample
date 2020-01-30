using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawSqlConsoleApp {
	public class AppDbContext : DbContext {
		private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => {
			builder
				.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
				.AddConsole();
		});

		public AppDbContext() {
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		public DbSet<Sample> Samples { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseLoggerFactory(_loggerFactory);

			var connectionString = new SqlConnectionStringBuilder {
				DataSource = ".",
				InitialCatalog = "Test",
				IntegratedSecurity = true,
			}.ToString();
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}
