using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest.Reactive {
	public class ObservableFilteringTest {
		private readonly ITestOutputHelper _output;

		public ObservableFilteringTest(ITestOutputHelper output) {
			_output = output;
		}

		[Fact]
		public void Where_試す() {
			// Arrange
			var values = new List<int>();

			// Act
			Observable.Range(0, 5)
				.Where(value => value % 2 == 0)
				.Subscribe(value => values.Add(value));

			// Assert
			Assert.Equal(new List<int> { 0, 2, 4 }, values);
		}
	}
}
