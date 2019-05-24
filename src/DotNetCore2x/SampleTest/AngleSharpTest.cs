using AngleSharp;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest {
	// AngleSharpを試してみる
	// https://anglesharp.github.io/
	// https://github.com/AngleSharp/AngleSharp/blob/master/doc/Basics.md
	public class AngleSharpTest {
		[Fact]
		public async Task OpenAsyncでドキュメントを読み込む() {
			// デフォルトの構成
			var config = Configuration.Default;

			// ブラウジングコンテキスト
			var context = BrowsingContext.New(config);

			// 解析するソース
			var source = "<h1>This is h1</h1>";

			// 仮想レスポンスを開いてドキュメントを読み込む
			var document = await context.OpenAsync(response => response.Content(source));

			Assert.Equal(source, document.Body.InnerHtml);
		}

		[Fact]
		public void Htmlパーサでドキュメントを読み込む() {
			var config = Configuration.Default;
			var context = BrowsingContext.New(config);
			var source = "<h1>This is h1</h1>";

			// HTMLパーサでドキュメントを読み込む
			var parser = context.GetService<IHtmlParser>();
			var document = parser.ParseDocument(source);

			Assert.Equal(source, document.Body.InnerHtml);
		}

		[Fact]
		public void QuerySelectorで要素を探す() {
			var config = Configuration.Default;
			var context = BrowsingContext.New(config);
			var source = "<h1>This is h1</h1>";
			var parser = context.GetService<IHtmlParser>();
			var document = parser.ParseDocument(source);

			// h1要素を探す
			var element = document.QuerySelector("h1");
			Assert.Equal("This is h1", element.TextContent);
		}
	}
}
