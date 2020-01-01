using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SampleTest {
	public class ValueTupleTest {
		[Fact(DisplayName = "ValueTuple_名前がないタプルはItem1、Item2といったフィルード名でアクセスできる")]
		public void ValueTuple_Unnamed() {
			var unnamed = ("a", 1);

			Assert.Equal("a", unnamed.Item1);
			Assert.Equal(1, unnamed.Item2);
		}

		[Fact(DisplayName = "ValueTuple_名前付きタプルは指定したフィールド名でアクセスできる")]
		public void ValueTuple_Named() {
			var named = (first: "a", second: 1);

			Assert.Equal("a", named.first);
			Assert.Equal(1, named.second);

			// Item1、Item2といったフィールド名でもアクセスできる
			Assert.Equal("a", named.Item1);
			Assert.Equal(1, named.Item2);
		}

		[Fact]
		public void ValueTuple_タプルのプロジェクション初期化子() {
			var x = 1;
			var y = 2;

			// プロジェクション初期化子
			// 暗黙的な名前が射影されるというやつ
			var tuple = (x, y);

			Assert.Equal(x, tuple.x);
			Assert.Equal(y, tuple.y);
		}

		[Fact]
		public void ValueTuple_タプルは比較できる() {
			var left = (x: 1, y: 2);
			var right = (x: 1, y: 2);

			Assert.True(left == right);
		}
	}
}
