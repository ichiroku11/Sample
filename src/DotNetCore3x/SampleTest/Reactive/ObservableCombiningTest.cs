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

		[Fact]
		public void Zip_シーケンスをマージする() {
			// Arrange
			var values = new List<int>();

			// Act
			Observable.Range(1, 3)
				.Zip(
					Observable.Repeat(2, 3),
					(first, second) => first + second)
				.Subscribe(value => values.Add(value));

			// Assert
			Assert.Equal(new List<int> { 3, 4, 5 }, values);
		}

		[Fact]
		public void Zip_どちらかのシーケンスが完了した段階で完了する() {
			// Arrange
			var values = new List<int>();

			// Act
			// 1つ目はシーケンスは3つ発行して完了
			// 2つ目のシーケンスは2つ発行して完了
			Observable.Range(1, 3)
				.Zip(
					Observable.Repeat(2, 2),
					(first, second) => first + second)
				.Subscribe(value => values.Add(value));

			// Assert
			Assert.Equal(new List<int> { 3, 4 }, values);
		}
	}
}
