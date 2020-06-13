using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using Xunit;

namespace SampleTest {
	public class TypeConverterTest {
		public enum Fruit {
			None = 0,
			Apple,
			Banana,
		}

		private static readonly IEnumerable<Fruit> _fruits = Enum.GetValues(typeof(Fruit)).Cast<Fruit>();

		public static IEnumerable<object[]> GetFruits() => _fruits.Select(fruit => new object[] { fruit });

		[Theory]
		[MemberData(nameof(GetFruits))]
		public void IsValid_enum値が定義されていることを判定できる(Fruit fruit) {
			var converter = TypeDescriptor.GetConverter(typeof(Fruit));

			Assert.True(converter.IsValid(fruit));
		}

		[Fact]
		public void IsValid_enum値が定義されている数値はtrueになる() {
			var converter = TypeDescriptor.GetConverter(typeof(Fruit));

			Assert.True(converter.IsValid(1));
		}

		[Theory]
		[InlineData((int)Fruit.None - 1)]
		[InlineData((int)Fruit.Banana + 1)]
		public void IsValid_enum値が定義されていない数値はfalseになる(int nofruit) {
			var converter = TypeDescriptor.GetConverter(typeof(Fruit));

			Assert.False(converter.IsValid(nofruit));
		}
	}
}
