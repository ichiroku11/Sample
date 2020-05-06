import { Assert, Test } from "../unittestlib";

export const unitTestLibTest = new Test()
	.fact("fact_テストが成功することを確認する", () => {
		// Arrange
		// Act
		// Assert
		Assert.equal("abc", "abc");
	});
/*
	.fact("fact_テストが失敗することを確認する", () => {
		// Arrange
		// Act
		// Assert
		Assert.equal("aby", "xyz");
	});
*/
