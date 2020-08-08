using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SampleTest.Linq {
	public class EnumerableSelectManyTest {
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
