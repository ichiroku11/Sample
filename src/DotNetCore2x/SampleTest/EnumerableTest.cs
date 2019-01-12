using System;
using System.Linq;
using Xunit;

namespace SampleTest {
	public class EnumerableTest {
		[Fact]
		public void Prepend_�V�[�P���X�̍ŏ��ɗv�f��ǉ�����() {
			// Arrange
			var source = new[] { 2, 3, 4 };

			// Act
			var actual = source.Prepend(1);

			// Assert
			Assert.Equal(new[] { 1, 2, 3, 4 }, actual);
		}

		[Fact]
		public void Append_�V�[�P���X�̍Ō�ɗv�f��ǉ�����() {
			// Arrange
			var source = new[] { 2, 3, 4 };

			// Act
			var actual = source.Append(5);

			// Assert
			Assert.Equal(new[] { 2, 3, 4, 5 }, actual);
		}
	}
}
