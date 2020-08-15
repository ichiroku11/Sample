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
		public void Empty_onCompletedだけが呼ばれる() {
			// Arrange
			var completed = false;

			// Act
			Observable.Empty<int>().Subscribe(
				onNext: _ => Assert.False(true),
				onError: _ => Assert.False(true),
				// onCompletedだけが呼ばれる
				onCompleted: () => {
					Assert.False(completed);
					completed = true;
				});

			// Assert
			Assert.True(completed);
		}

		[Fact]
		public void Never_何も呼ばれない() {
			// Arrange
			var next = false;
			var error = false;
			var completed = false;

			// Act
			Observable.Never<int>().Subscribe(
				onNext: _ => next = true,
				onError: _ => error = true,
				onCompleted: () => completed = true);

			// Assert
			Assert.False(next);
			Assert.False(error);
			Assert.False(completed);
		}

		[Fact]
		public void Range_onNextとonCompletedが呼ばれる() {
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
				onError: _ => {
					// エラーが発生しないので呼ばれない
					Assert.False(true);
				},
				onCompleted: () => {
					_output.WriteLine($"onCompleted");
					// onCompletedは初めて呼ばれる
					Assert.False(completed);

					completed = true;
				});

			// Assert
			Assert.Equal(new List<int>() { 0, 1, 2 }, values);
			Assert.True(completed);
		}

		[Fact]
		public void Throw_onErrorだけが呼ばれる() {
			// Arrange
			var expected = new Exception();
			var actual = default(Exception);

			// Act
			Observable.Throw<int>(expected).Subscribe(
				onNext: _ => Assert.False(true),
				// onErrorだけが呼ばれる
				onError: exception => actual = exception,
				onCompleted: () => Assert.True(false));

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
