using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest {
	public class DatabaseFacadeTest {
		[Fact]
		public async Task ExecuteSqlRawAsync_select文の結果を取得できない() {
			using var context = new AppDbContext();

			var result = await context.Database.ExecuteSqlRawAsync("select 1");

			// 影響を受けた行はない
			Assert.Equal(-1, result);
		}

		[Fact]
		public async Task ExecuteSqlRawAsync_出力パラメータを使って結果を取得する() {
			using var context = new AppDbContext();

			// 出力パラメータ
			var param = new SqlParameter {
				Direction = ParameterDirection.Output,
				ParameterName = "result",
				SqlDbType = SqlDbType.Int,
			};
			var result = await context.Database.ExecuteSqlRawAsync("set @result = (select 1)", param);

			// 影響を受けた行はない
			Assert.Equal(-1, result);
			// 出力パラメータから値を取得できる
			Assert.Equal(1, param.Value);
		}
	}
}
