using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SampleTest.Linq {
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

		[Fact]
		public void SequenceEqual_2つのコレクションが等しいか比較できる() {
			// Arrange
			var first = new[] { 1, 2, 3 };
			var second = new[] { 1, 2, 3 };

			// Act
			// Assert
			Assert.True(first.SequenceEqual(second));
		}

		private interface ISample {
		}

		private class Sample : ISample {
		}

		[Fact]
		public void ToList_クラスのListをインターフェイスのIListに変換する() {
			// Arrange
			var items = new List<Sample> { new Sample() };

			// Act
			// Assert
			Assert.True(items.ToList<ISample>() is List<ISample>);
		}

		private class Item {
			public Item(string name, IEnumerable<Item> children = default) {
				Name = name;
				Children = children ?? Enumerable.Empty<Item>();
			}

			public string Name { get; }

			public IEnumerable<Item> Children { get; }
		}

		private static readonly IEnumerable<Item> _items
			= new[] {
				new Item("1a", new[] {
					new Item("2a", new[] {
						new Item("3a"),
						new Item("3b"),
					}),
					new Item("2b", new[] {
						new Item("3c"),
					}),
				}),
				new Item("1b", new[] {
					new Item("2c", new[] {
						new Item("3d"),
					}),
				}),
			};

		[Fact]
		public void SelectMany_2階層目を平坦化するサンプル() {
			// Arrange
			// Act
			var actual = _items
				.SelectMany(item => item.Children)
				.Select(item => item.Name);

			// Assert
			IEnumerable<string> expected = new[] {
				"2a", "2b", "2c",
			};
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void SelectMany_3階層目を平坦化するサンプル() {
			// Arrange
			// Act
			var actual = _items
				.SelectMany(item => item.Children)
				.SelectMany(item => item.Children)
				.Select(item => item.Name);

			// Assert
			IEnumerable<string> expected = new[] {
				"3a", "3b", "3c", "3d"
			};
			Assert.Equal(expected, actual);
		}
	}
}
