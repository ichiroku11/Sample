using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest {
	public class DbSetRawSqlTest {
		[Fact]
		public async Task FromSqlRaw_パラメータを使わないクエリ() {
			using var context = new AppDbContext();

			var samples = await context
				.Samples
				.FromSqlRaw("select 1 as Id, N'a' as Name")
				// ここでSQLを実行する
				.ToListAsync();
			var sample = samples.First();

			Assert.Equal(1, sample.Id);
			Assert.Equal("a", sample.Name);
		}
	}
}
