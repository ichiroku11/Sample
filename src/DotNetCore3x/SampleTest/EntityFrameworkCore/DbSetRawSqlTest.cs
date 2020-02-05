using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest.EntityFrameworkCore {
	// 参考
	// https://docs.microsoft.com/ja-jp/ef/core/querying/raw-sql
	public class DbSetRawSqlTest {
		#region FromSqlInterpolated
		[Fact]
		public async Task FromSqlInterpolated_パラメータを使わないクエリ() {
			using var context = new AppDbContext();

			var samples = await context.Samples
				// パラメータを使わないとあまり補間の意味が無いかも
				.FromSqlInterpolated($"select 1 as Id, N'a' as Name")
				// ネストされたSQL文にならないように
				// ここでSQLを実行する
				.ToListAsync();
			var sample = samples.First();

			Assert.Equal(1, sample.Id);
			Assert.Equal("a", sample.Name);
		}

		[Fact]
		public async Task FromSqlInterpolated_パラメータを使ったクエリその1() {
			using var context = new AppDbContext();

			var samples = await context.Samples
				.FromSqlInterpolated($"select {1} as Id, {"a"} as Name")
				.ToListAsync();
			// 実行されるSQL
			// select @p0 as Id, @p1 as Name
			var sample = samples.First();

			Assert.Equal(1, sample.Id);
			Assert.Equal("a", sample.Name);
		}

		// その1と同じ
		[Fact]
		public async Task FromSqlInterpolated_パラメータを使ったクエリその2() {
			using var context = new AppDbContext();

			// 同じ
			//var param = (id: 1, name: "a");
			var param = new { id = 1, name = "a" };
			var samples = await context.Samples
				.FromSqlInterpolated($"select {param.id} as Id, {param.name} as Name")
				.ToListAsync();
			// 実行されるSQL
			// select @p0 as Id, @p1 as Name
			var sample = samples.First();

			Assert.Equal(1, sample.Id);
			Assert.Equal("a", sample.Name);
		}
		#endregion

		#region FromSqlRaw
		[Fact]
		public async Task FromSqlRaw_パラメータを使わないクエリ() {
			using var context = new AppDbContext();

			var samples = await context.Samples
				.FromSqlRaw("select 1 as Id, N'a' as Name")
				.ToListAsync();
			var sample = samples.First();

			Assert.Equal(1, sample.Id);
			Assert.Equal("a", sample.Name);
		}

		[Fact]
		public async Task FromSqlRaw_パラメータを使ったクエリ() {
			using var context = new AppDbContext();

			var samples = await context.Samples
				.FromSqlRaw(
					"select @id as Id, @name as Name",
					new SqlParameter("id", 1), new SqlParameter("name", "a"))
				.ToListAsync();
			var sample = samples.First();

			Assert.Equal(1, sample.Id);
			Assert.Equal("a", sample.Name);
		}

		[Fact]
		public async Task FromSqlRaw_HasQueryFilterを確認する() {
			using var context = new AppDbContext();

			var samples = await context.Samples
				.FromSqlRaw("select 1 as Id, null as Name")
				.ToListAsync();
			var sample = samples.FirstOrDefault();

			Assert.Null(sample);
		}

		[Fact]
		public async Task FromSqlRaw_IgnoreQueryFiltersを確認する() {
			using var context = new AppDbContext();

			var samples = await context.Samples
				.FromSqlRaw("select 1 as Id, null as Name")
				.IgnoreQueryFilters()
				.ToListAsync();
			var sample = samples.FirstOrDefault();

			Assert.Equal(1, sample.Id);
			Assert.Null(sample.Name);
		}
		#endregion
	}
}
