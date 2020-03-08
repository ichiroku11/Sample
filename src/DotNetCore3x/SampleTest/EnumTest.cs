using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace SampleTest {
	public class EnumTest {
		private enum Fruit {
			[Display(Name = "りんご")]
			Apple = 1,

			[Display(Name = "バナナ")]
			Banana,

			[Display(Name = "オレンジ")]
			Orange,
		}

		private static readonly IEnumerable<Fruit> _fruits
			= new[] {
				Fruit.Apple,
				Fruit.Banana,
				Fruit.Orange
			};

		[Fact]
		public void IsDefined_数値を識別できる() {
			// Arrange
			// Act
			// Assert

			// 定義されている
			Assert.True(Enum.IsDefined(typeof(Fruit), (int)Fruit.Apple));

			// 定義されていない
			Assert.False(Enum.IsDefined(typeof(Fruit), (int)Fruit.Orange + 1));
		}

		[Fact]
		public void IsDefined_文字列を識別できる() {
			// Arrange
			// Act
			// Assert
			Assert.True(Enum.IsDefined(typeof(Fruit), "Apple"));
		}

		[Fact]
		public void GetValues_enum値を列挙できる() {
			// Arrange
			// Act
			var fruits = Enum.GetValues(typeof(Fruit)).OfType<Fruit>();

			// Assert
			Assert.Equal(_fruits, fruits);
		}

		[Fact]
		public void リフレクションでenum値を列挙する() {
			// Arrange
			// Act
			var fruits = typeof(Fruit)
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Select(field => (Fruit)field.GetValue(null));

			// Assert
			Assert.Equal(_fruits, fruits);
		}

		[Fact]
		public void リフレクションでDisplayAttributeを取得する() {
			// Arrange
			// Act
			var displayAttribute = typeof(Fruit)
				.GetField(nameof(Fruit.Apple))
				.GetCustomAttributes<DisplayAttribute>()
				.First();

			// Assert
			Assert.Equal("りんご", displayAttribute.Name);
		}
	}
}
