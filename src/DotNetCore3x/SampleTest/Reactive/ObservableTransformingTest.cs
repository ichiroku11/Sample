using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest.Reactive {
	public class ObservableTransformingTest {
		private readonly ITestOutputHelper _output;

		public ObservableTransformingTest(ITestOutputHelper output) {
			_output = output;
		}

		[Fact]
		public void Select_試す() {
			// Arrange
			var values = new List<int>();

			// Act
			Observable.Return(2)
				.Select(value => value * value)
				.Subscribe(value => values.Add(value));

			// Assert
			Assert.Equal(new List<int> { 4 }, values);
		}

	}
}
