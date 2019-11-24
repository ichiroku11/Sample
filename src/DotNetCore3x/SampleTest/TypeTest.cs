using System;
using Xunit;

namespace SampleTest {
	public class TypeTest {
		private class Base {
		}

		private interface IInterface {
		}

		private class Derived : Base, IInterface {
		}

		[Fact]
		public void IsAssignableFrom_確認する() {
			var baseType = typeof(Base);
			var derivedType = typeof(Derived);
			var interfaceType = typeof(IInterface);

			// Base型の変数に、Derived型のインスタンスを割り当てることができる
			Assert.True(baseType.IsAssignableFrom(derivedType));

			// Derived型の変数に、Base型のインスタンスを割り当てることができる
			Assert.False(derivedType.IsAssignableFrom(baseType));

			// Base型の変数に、Base型のインスタンスを割り当てることができる
			Assert.True(baseType.IsAssignableFrom(baseType));

			// IInterface型の変数に、Derived型のインスタンスを割り当てることができる
			Assert.True(interfaceType.IsAssignableFrom(derivedType));
		}
	}
}
