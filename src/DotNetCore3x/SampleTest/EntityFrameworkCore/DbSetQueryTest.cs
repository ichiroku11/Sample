using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest.EntityFrameworkCore {
	public class DbSetQueryTest : IDisposable {
		private AppDbContext _context;

		public DbSetQueryTest() {
			_context = new AppDbContext();

			Init();
		}

		public void Dispose() {
			if (_context != null) {
				_context.Dispose();
				_context = null;
			}
		}

		private void Init() {
			// 準備
			var sql = @"
drop table if exists dbo.Sample;

create table dbo.Sample(
	Id int,
	Name nvarchar(10),
	constraint PK_Sample primary key(Id)
);

insert into dbo.Sample(Id, Name)
output inserted.*
values
	(1, N'a'),
	(2, N'b');";
			_context.Database.ExecuteSqlRaw(sql);
		}

		[Fact]
		public async Task FindAsync_主キーで検索できる() {
			var sample = await _context.Samples.FindAsync(1);

			Assert.Equal(1, sample.Id);
			Assert.Equal("a", sample.Name);
		}

		[Fact]
		public async Task FindAsync_主キーで検索して見つからない場合はnull() {
			var sample = await _context.Samples.FindAsync(0);

			Assert.Null(sample);
		}

		[Fact]
		public async Task FirstOrDefaultAsync_述語を使って検索できる() {
			var sample = await _context.Samples.FirstOrDefaultAsync(entity => entity.Id == 1);

			Assert.Equal(1, sample.Id);
			Assert.Equal("a", sample.Name);
		}
	}
}
