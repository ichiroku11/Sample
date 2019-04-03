using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BasicAuthWebApp.Test {
	public class BasicCredentialsHelperTest {
		// 意味のあるテストかな
		[Fact]
		public void Encode_TryDecode_エンコードしてデコードできる() {
			// Arrange
			// Act
			var credentials = BasicCredentialsHelper.Encode("user", "password");
			var result = BasicCredentialsHelper.TryDecode(credentials, out var userName, out var password);

			// Assert
			Assert.True(result);
			Assert.Equal("user", userName);
			Assert.Equal("password", password);
		}
	}
}
