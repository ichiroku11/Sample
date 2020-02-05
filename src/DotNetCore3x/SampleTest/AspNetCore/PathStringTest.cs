using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace SampleTest.AspNetCore {
	public class PathStringTest {
		[Fact]
		public void PathString_とりあえず使ってみる() {
			// Arrange
			// Act
			var path = new PathString("/app");

			// Assert
			Assert.Equal("/app", path.Value);
		}

		[Fact(DisplayName = "PathString_コンストラクタ引数は'/'で始まらないとArgumentExceptionがスローされる")]
		public void PathString_ConstructorArgumentStartWithSlash() {
			// Arrange
			// Act
			Assert.Throws<ArgumentException>(() => {
				new PathString("app");
			});

			// Assert
			//Assert.Equal("/app", path.Value);
		}
	}
}
