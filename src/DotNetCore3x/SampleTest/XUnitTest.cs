using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest {
	// xUnit.netの使い方
	public class XUnitTest {
		// デバッグのメッセージの出力用
		private readonly ITestOutputHelper _output;

		public XUnitTest(ITestOutputHelper output) {
			_output = output;

			// 初期化処理はコンストラクタで行う
			// テストごとに呼ばれる
			_output.WriteLine("Setup");
		}

		public void Dispose() {
			// 後処理はDisposeで行う
			// テストごとに呼ばれる
			_output.WriteLine("Teardown");
		}

		// パラメータを使わないテスト
		[Fact]
		public void Fact属性でパラメータを使わないテスト() {
			Assert.True(true);
		}

		// パラメータを使ったテスト
		[Theory]
		// テストデータを埋め込む
		[InlineData(2)]
		[InlineData(4)]
		public void Theory属性とInlineData属性でパラメータを使ったテスト(int value) {
			Assert.Equal(0, value % 2);
		}

	}
}
