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
		public void Subscribe_動きを確認する() {
			// Arrange
			var values = new List<int>();
			var completed = false;

			// Act
			Observable.Range(0, 3).Subscribe(
				onNext: value => {
					// 0, 1, 2と呼ばれる
					_output.WriteLine($"onNext: {value}");

					// onCompletedは呼ばれていない
					Assert.False(completed);

					values.Add(value);
				},
				onError: exception => {
					// エラーが発生しないので呼ばれない
					Assert.False(true);
				},
				onCompleted: () => {
					_output.WriteLine($"onCompleted");
					// onCompletedば初めて呼ばれる
					Assert.False(completed);

					completed = true;
				});

			// Assert
			Assert.Equal(new List<int>() { 0, 1, 2 }, values);
			Assert.True(completed);
		}

	}
}
