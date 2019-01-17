using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest.Reactive {
	public class ObservableTest {
		private readonly ITestOutputHelper _output;

		public ObservableTest(ITestOutputHelper output) {
			_output = output;
		}

		[Fact]
		public void Range_Subscribeを試す() {
			// Arrange
			var values = new List<int>();
			var completed = false;

			// Act
			Observable.Range(0, 3).Subscribe(
				value => {
					_output.WriteLine($"onNext: {value}");
					Assert.False(completed);

					values.Add(value);
				},
				exception => {
					// エラーが発生しないので呼ばれない
					Assert.True(false);
				},
				() => {
					_output.WriteLine($"onCompleted");
					Assert.False(completed);

					completed = true;
				});

			// Assert
			Assert.Equal(new List<int>() { 0, 1, 2, }, values);
			Assert.True(completed);
		}
	}
}
