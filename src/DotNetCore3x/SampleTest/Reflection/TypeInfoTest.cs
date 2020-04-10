using System;
using System.Collections.Generic;
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
		public void GetCustomAttributes_CompilerGeneratedAttribute() {
			// Arrange
			// Act
			var atributes = typeof(Sample).GetTypeInfo()
				.GetCustomAttributes<CompilerGeneratedAttribute>();

			// Assert
		}
	}
}
