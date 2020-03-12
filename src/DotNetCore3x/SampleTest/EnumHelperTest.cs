using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace SampleTest {
	public class EnumHelperTest {
		private enum Fruit {
			None = 0,

			[Display(Name = "りんご")]
			Apple,

			[Display(Name = "バナナ")]
			Banana,
		}

		[Fact]
		public void GetAttributes_取得できる() {
			// Arrange
			// Act
			var attributes = EnumHelper<Fruit>.GetAttributes<DisplayAttribute>();

			// Assert
			Assert.Equal(3, attributes.Count);
			Assert.Null(attributes[Fruit.None]);
			Assert.Equal("りんご", attributes[Fruit.Apple].Name);
			Assert.Equal("バナナ", attributes[Fruit.Banana].Name);
		}
	}
}
