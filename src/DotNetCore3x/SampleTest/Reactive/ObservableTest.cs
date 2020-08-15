using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
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
				onNext: _ => AssertHelper.Fail(),
				onError: _ => AssertHelper.Fail(),
				// onCompletedだけが呼ばれる
				onCompleted: () => {
					Assert.False(completed);
					completed = true;
				});

			// Assert
			Assert.True(completed);
		}

		[Fact]
		public void FromAsync_TaskをObservableに変換するとonNextとonCompletedが呼ばれる() {
			// Arrange
			var expected = 11;
			var actual = 0;
			var completed = false;

			// Act
			Observable.FromAsync(() => Task.FromResult(expected)).Subscribe(
				onNext: value => {
					actual = value;
				},
				onCompleted: () => {
					Assert.False(completed);
					completed = true;
				});

			// Assert
			Assert.True(completed);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Never_何も呼ばれない() {
			// Arrange
			// Act
			// Assert
			Observable.Never<int>().Subscribe(
				onNext: _ => AssertHelper.Fail(),
				onError: _ => AssertHelper.Fail(),
				onCompleted: () => AssertHelper.Fail());
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
					AssertHelper.Fail();
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
		public void Repeat_onNextとonCompletedが呼ばれる() {
			// Arrange
			var values = new List<int>();
			var completed = false;

			// Act
			Observable.Repeat(1, 3).Subscribe(
				onNext: value => {
					Assert.False(completed);

					_output.WriteLine($"onNext: {value}");
					values.Add(value);
				},
				onError: _ => {
					AssertHelper.Fail();
				},
				onCompleted: () => {
					Assert.False(completed);

					_output.WriteLine($"onCompleted");
					completed = true;
				});

			// Assert
			Assert.Equal(new List<int>() { 1, 1, 1 }, values);
			Assert.True(completed);
		}

		[Fact]
		public void Return_onNextとonCompletedが呼ばれる() {
			// Arrange
			var values = new List<int>();
			var completed = false;

			// Act
			Observable.Return(1).Subscribe(
				onNext: value => {
					Assert.False(completed);

					_output.WriteLine($"onNext: {value}");
					values.Add(value);
				},
				onError: _ => {
					AssertHelper.Fail();
				},
				onCompleted: () => {
					Assert.False(completed);

					_output.WriteLine($"onCompleted");
					completed = true;
				});

			// Assert
			Assert.Equal(new List<int>() { 1 }, values);
			Assert.True(completed);
		}

		[Fact]
		public void Throw_onErrorだけが呼ばれる() {
			// Arrange
			var expected = new Exception();
			var actual = default(Exception);

			// Act
			Observable.Throw<int>(expected).Subscribe(
				onNext: _ => AssertHelper.Fail(),
				// onErrorだけが呼ばれる
				onError: exception => actual = exception,
				onCompleted: () => AssertHelper.Fail());

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}
