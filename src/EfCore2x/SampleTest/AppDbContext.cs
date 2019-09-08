using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTest {
	public class AppDbContext : DbContext {
		public static AppDbContext Create() {
			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseInMemoryDatabase(nameof(InMemoryDbTest))
				.Options;

			return new AppDbContext(options);
		}

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options) {
		}

		public DbSet<Monster> Monsters { get; set; }
	}
}
