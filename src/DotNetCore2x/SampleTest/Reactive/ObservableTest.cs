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
					// 0, 1, 2と呼ばれる
					_output.WriteLine($"onNext: {value}");
					Assert.False(completed);

					values.Add(value);
				},
				exception => {
					// エラーが発生しないので呼ばれない
					Assert.False(true);
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

		[Fact]
		public void Empty_onCompletedだけが呼ばれる() {
			Observable.Empty<int>().Subscribe(
				value => Assert.False(true),
				exception => Assert.False(true),
				// onCompletedだけ
				() => Assert.True(true));
		}

		[Fact]
		public void Throw_onErrorだけが呼ばれる() {
			var original = new Exception();
			Observable.Throw<int>(original).Subscribe(
				value => Assert.False(true),
				// onErrorだけ
				exception => Assert.Equal(exception, original),
				() => Assert.True(false));
		}

		[Fact]
		public void Never_何も呼ばれない() {
			// 何も呼ばれない
			Observable.Never<int>().Subscribe(
				value => Assert.False(true),
				exception => Assert.False(true),
				() => Assert.False(true));
		}
	}
}
