using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

		private enum ConvertType {
			// enum <=> tinyint
			Default = 0,
			// enum <=> varchar(10)
			String,
			// enum <=> char(2)
			Char2,
		}

		private class MonsterDbContext : AppDbContext {
			private readonly ConvertType _convertType;
			public MonsterDbContext(ConvertType convertType) {
				_convertType = convertType;
			}

			public DbSet<Monster> Monsters { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder) {
				modelBuilder.Entity<Monster>().ToTable(nameof(Monster));

				// todo:
				/*
				modelBuilder.Entity<Monster>().ToTable(nameof(Monster))
					.Property(entity => entity.Category).HasConversion<string>();
				*/
			}
		}
	}
}
