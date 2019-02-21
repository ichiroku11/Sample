using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SampleTest {
	public class EnumTest {
		private enum Fruit {
			Apple = 0,
			Banana,
		}

		[Fact]
		public void IsDefined_数値を識別できる() {
			// Arrange
			// Act
			// Assert
			Assert.True(Enum.IsDefined(typeof(Fruit), (int)Fruit.Apple));
			Assert.False(Enum.IsDefined(typeof(Fruit), (int)Fruit.Banana + 1));
		}

		[Fact]
		public void IsDefined_文字列を識別できる() {
			// Arrange
			// Act
			// Assert
			Assert.True(Enum.IsDefined(typeof(Fruit), "Apple"));
		}
	}
}
