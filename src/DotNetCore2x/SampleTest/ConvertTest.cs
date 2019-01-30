using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SampleTest {
	public class ConvertTest {
		[Theory]
		[InlineData("1", 1)]
		[InlineData("0001", 1)]
		[InlineData("10", 2)]
		[InlineData("11111111", 255)]
		public void ToInt32_2進数文字列をInt32に変換できる(string text, int expected) {
			// Arrange
			// Act
			var actual = Convert.ToInt32(text, 2);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("2")]
		[InlineData("a")]
		public void ToInt32_2進数文字列をInt32に変換できずFormatExceptionがスローされる(string text) {
			// Arrange
			// Act
			// Assert
			Assert.Throws<FormatException>(() => Convert.ToInt32(text, 2));
		}
	}
}
