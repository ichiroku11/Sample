using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Xunit;

namespace SampleTest.Reactive {
	public class ObservableCombiningTest {
		[Fact]
		public void StartWith_シーケンスの前に発行する() {
			// Arrange
			var values = new List<int>();

			// Act
			Observable.Return(3)
				.StartWith(1, 2)
				.Subscribe(value => values.Add(value));

			// Assert
			Assert.Equal(new List<int> { 1, 2, 3 }, values);
		}
	}
}
