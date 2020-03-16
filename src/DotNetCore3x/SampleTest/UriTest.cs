using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SampleTest {
	public class UriTest {
		[Theory]
		[InlineData("http://example.jp", "http")]
		[InlineData("https://example.jp", "https")]
		public void Scheme_スキーマを取得できる(string url, string scheme) {
			// Arrange
			var uri = new Uri(url);

			// Act
			// Assert
			Assert.Equal(scheme, uri.Scheme);
		}

		[Theory]
		[InlineData("xyz")]
		public void TryCreate_絶対URLの作成に失敗する(string url) {
			// Arrange
			// Act
			var result = Uri.TryCreate(url, UriKind.Absolute, out var _);

			// Assert
			Assert.False(result);
		}

		[Theory]
		[InlineData("https://example.jp/path1/path2", new[] { "/", "path1/", "path2" })]
		[InlineData("https://example.jp/path1/path2/", new[] { "/", "path1/", "path2/" })]
		public void Segments_パス部分を配列として取得できる(string url, string[] segments) {
			// Arrange
			var uri = new Uri(url);

			// Act
			// Assert
			Assert.Equal(segments, uri.Segments);
		}

		[Theory]
		[InlineData("https://example.jp#fragment", "#fragment")]
		public void Fragment_フラグメントを取得できる(string url, string fragment) {
			// Arrange
			var uri = new Uri(url);

			// Act
			// Assert
			Assert.Equal(fragment, uri.Fragment);
		}
	}
}
