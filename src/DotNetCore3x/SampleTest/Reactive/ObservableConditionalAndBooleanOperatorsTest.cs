using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using Xunit;

namespace SampleTest.Reactive {
	public class ObservableConditionalAndBooleanOperatorsTest {
		// http://reactivex.io/documentation/operators/all.html
		[Fact]
		public void All_発行されたすべてのアイテムが条件を満たすかどうかを判断する() {
			// Arrange
			var values = new List<bool>();

			// Act
			Observable.Range(0, 5)
				.All(value => value <= 4)
				.Subscribe(value => values.Add(value));

			// Assert
			Assert.True(values.Single());
		}
	}
}
