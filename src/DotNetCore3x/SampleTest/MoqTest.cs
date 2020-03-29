using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SampleTest {
	// Moqの使い方
	// 参考
	// https://qiita.com/usamik26/items/42959d8b95397d3a8ffb
	public class MoqTest {
		// テスト対象
		public interface ITestTarget<TValue> {
			TValue SomeValue { get; set; }
		}

		[Fact]
		public void Setup_固定値を返すプロパティをエミュレートする() {
			var mock = new Mock<ITestTarget<int>>();

			// 固定値を返すプロパティ
			mock.Setup(target => target.SomeValue)
				.Returns(1);

			// セットアップした値を取得できる
			Assert.Equal(1, mock.Object.SomeValue);
		}

		[Fact]
		public void Setup_固定値を返すプロパティに値をセットしても変更されない() {
			var mock = new Mock<ITestTarget<int>>();

			// 固定値を返すプロパティ
			mock.Setup(target => target.SomeValue)
				.Returns(1);

			// プロパティの値を変更する
			mock.Object.SomeValue = 2;

			// 取得できるのはセットアップした値
			Assert.Equal(1, mock.Object.SomeValue);
		}
	}
}
