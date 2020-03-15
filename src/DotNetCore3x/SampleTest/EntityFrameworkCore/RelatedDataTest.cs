using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTest.EntityFrameworkCore {
	// 参考
	// https://docs.microsoft.com/ja-jp/ef/core/querying/related-data
	public class RelatedDataTest : IDisposable {
		private class MonsterCategory {
			public int Id { get; set; }
			public string Name { get; set; }
		}

		private class Monster {
			public int Id { get; set; }
			public int CategoryId { get; set; }
			public string Name { get; set; }
		}

		// todo:
		/*
		private class Item {
			public int Id { get; set; }
			public string Name { get; set; }
		}

		private class MonsterItem {
			public int MonsterId { get; set; }
			public int ItemId { get; set; }
		}
		*/

		private class MonsterDbContext : AppDbContext {
			// todo:
		}

		private MonsterDbContext _context;

		public RelatedDataTest() {
			_context = new MonsterDbContext();

			DropTable();
			InitTable();
		}

		public void Dispose() {
			DropTable();

			if (_context != null) {
				_context.Dispose();
				_context = null;
			}
		}

		private void InitTable() {
			// todo:
		}

		private void DropTable() {
			// todo:
		}


	}
}
