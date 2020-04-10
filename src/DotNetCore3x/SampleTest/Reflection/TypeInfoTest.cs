using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace SampleTest.Reflection {
	public class TypeInfoTest {
		public class Sample {
			public int Value { get; set; }
		}

		[Fact]
		public void GetterSetterプロパティは対応するメソッドが自動生成される() {
			// Arrange
			// Act
			var methods = typeof(Sample).GetTypeInfo()
				.GetMethods()
				.Where(method => method.GetCustomAttributes<CompilerGeneratedAttribute>().Any());

			// Assert
			Assert.Equal(2, methods.Count());
			Assert.Contains(methods, method => string.Equals(method.Name, $"get_{nameof(Sample.Value)}"));
			Assert.Contains(methods, method => string.Equals(method.Name, $"set_{nameof(Sample.Value)}"));
		}
	}
}
