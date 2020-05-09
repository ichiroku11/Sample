import { Assert, Test } from "../unittestlib";

// スプレッド構文
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Operators/Spread_syntax

export const spreadSyntaxTest = new Test("SpreadSyntaxTest")
	.fact("spread_配列で使ってみる", () => {
		// Arrange
		const items = [1, 2];
		// Act
		const actual = [0, ...items, 3];

		// Assert
		Assert.equal([0, 1, 2, 3], actual);
	})
	.fact("spread_配列を連結する", () => {
		// Arrange
		const items1 = [0, 1];
		const items2 = [2, 3];
		// Act
		const actual = [...items1, ...items2];

		// Assert
		Assert.equal([0, 1, 2, 3], actual);
	});
