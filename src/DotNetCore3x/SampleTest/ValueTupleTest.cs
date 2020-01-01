using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SampleTest {
	public class ValueTupleTest {
		[Fact(DisplayName = "名前がないタプルはItem1、Item2といったフィルード名でアクセスできる")]
		public void ValueTuple_Unnamed() {
			var unnamed = ("a", 1);

			Assert.Equal("a", unnamed.Item1);
			Assert.Equal(1, unnamed.Item2);
		}

		[Fact(DisplayName = "名前付きタプルは指定したフィールド名でアクセスできる")]
		public void ValueTuple_Named() {
			var named = (first: "a", second: 1);

			Assert.Equal("a", named.first);
			Assert.Equal(1, named.second);

			// Item1、Item2といったフィールド名でもアクセスできる
			Assert.Equal("a", named.Item1);
			Assert.Equal(1, named.Item2);
		}
	}
}
