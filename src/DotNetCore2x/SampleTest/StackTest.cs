using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SampleTest {
	public class StackTest {
		[Fact]
		public void Pop_コンストラクタで指定したコレクションの逆順で取得できる() {
			// Arrange
			// Act
			var stack = new Stack<int>(new[] { 1, 2, 3 });

			// Assert
			Assert.Equal(3, stack.Pop());
			Assert.Equal(2, stack.Pop());
			Assert.Equal(1, stack.Pop());
		}
	}
}
