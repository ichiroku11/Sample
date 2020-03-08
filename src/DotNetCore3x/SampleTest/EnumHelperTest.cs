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
		public void GetDisplayAttributes_取得できる() {
			// Arrange
			// Act
			var attributes = EnumHelper.GetDisplayAttributes<Fruit>();

			// Assert
			Assert.Equal(3, attributes.Count);
			Assert.Null(attributes[Fruit.None]);
			Assert.Equal("りんご", attributes[Fruit.Apple].Name);
			Assert.Equal("バナナ", attributes[Fruit.Banana].Name);
		}
	}
}
