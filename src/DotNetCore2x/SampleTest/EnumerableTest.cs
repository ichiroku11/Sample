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

		[Fact]
		public void Min_��null�̋�̃V�[�P���X�ŌĂяo����InvalidOperationException() {
			// ��null�̏ꍇ�͗�O
			Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Min());

			// null���e�^�̏ꍇ��null
			Assert.Null(Enumerable.Empty<int?>().Min());
		}

		[Fact]
		public void Max_��null�̋�̃V�[�P���X�ŌĂяo����InvalidOperationException() {
			// ��null�̏ꍇ�͗�O
			Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Max());

			// null���e�^�̏ꍇ��null
			Assert.Null(Enumerable.Empty<int?>().Max());
		}

		[Fact]
		public void Average_��null�̋�̃V�[�P���X�ŌĂяo����InvalidOperationException() {
			// ��null�̏ꍇ�͗�O
			Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().Average());

			// null���e�^�̏ꍇ��null
			Assert.Null(Enumerable.Empty<int?>().Average());
		}
	}
}
