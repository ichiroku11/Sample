﻿using System;
using System.Linq;
using Xunit;

namespace SampleTest {
	public class EnumerableTest {
		[Fact]
		public void Prepend_シーケンスの最初に要素を追加する() {
			// Arrange
			var source = new[] { 2, 3, 4 };

			// Act
			var actual = source.Prepend(1);

			// Assert
			Assert.Equal(new[] { 1, 2, 3, 4 }, actual);
		}

		[Fact]
		public void Append_シーケンスの最後に要素を追加する() {
			// Arrange
			var source = new[] { 2, 3, 4 };

			// Act
			var actual = source.Append(5);

			// Assert
			Assert.Equal(new[] { 2, 3, 4, 5 }, actual);
		}

		[Fact]
		public void Min_非nullの空のシーケンスで呼び出すとInvalidOperationException() {
			// 非nullの場合は例外
			Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Min());

			// null許容型の場合はnull
			Assert.Null(Enumerable.Empty<int?>().Min());
		}

		[Fact]
		public void Max_非nullの空のシーケンスで呼び出すとInvalidOperationException() {
			// 非nullの場合は例外
			Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Max());

			// null許容型の場合はnull
			Assert.Null(Enumerable.Empty<int?>().Max());
		}

		[Fact]
		public void Average_非nullの空のシーケンスで呼び出すとInvalidOperationException() {
			// 非nullの場合は例外
			Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Average());

			// null許容型の場合はnull
			Assert.Null(Enumerable.Empty<int?>().Average());
		}

		[Fact]
		public void Take_引数にマイナスの値を指定すると空のシーケンスが返ってくる() {
			// Arrange
			var source = new[] { 1, 2, 3 };

			// Act
			var actual = source.Take(-1);

			// Assert
			Assert.Empty(actual);
		}

		[Fact]
		public void Skip_引数にマイナスの値を指定すると同じシーケンスが返ってくる() {
			// Arrange
			var source = new[] { 1, 2, 3 };

			// Act
			var actual = source.Skip(-1);

			// Assert
			Assert.NotSame(source, actual);
			Assert.Equal(source, actual);
		}
	}
}
