using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest {
	public class InMemoryDbTest {
		[Fact]
		public async Task 追加したエンティティを取得できる() {
			using (var context = AppDbContext.Create()) {
				// 追加
				context.Monsters.Add(new Monster {
					Id = 1,
					Name = "スライム",
				});

				var result = await context.SaveChangesAsync();
				// 件数が1件
				Assert.Equal(1, result);
			}

			using (var context = AppDbContext.Create()) {
				// 取得
				var count = await context.Monsters.CountAsync();
				Assert.Equal(1, count);

				var monster = await context.Monsters.SingleAsync();
				Assert.Equal(1, monster.Id);
				Assert.Equal("スライム", monster.Name);
			}
		}
	}
}
