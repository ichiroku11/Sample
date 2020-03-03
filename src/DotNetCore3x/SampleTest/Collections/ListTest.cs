using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SampleTest.Collections {
	public class ListTest {
		private interface ISample {
		}

		private class Sample : ISample {
		}

		[Fact]
		public void ToList_クラスのListをインターフェイスのIListに変換する() {
			// Arrange
			var items = new List<Sample> { new Sample() };

			// Act
			// Assert
			Assert.NotNull(items.ToList<ISample>() as IList<ISample>);
		}
	}
}
