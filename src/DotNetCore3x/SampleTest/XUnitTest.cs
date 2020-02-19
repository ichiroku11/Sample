using System;
using System.Collections.Generic;
using System.Text;
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
	}
}
