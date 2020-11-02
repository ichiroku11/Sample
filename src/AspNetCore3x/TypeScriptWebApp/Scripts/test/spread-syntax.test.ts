import { Assert, Test } from "../unittestlib";

// スプレッド構文
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Operators/Spread_syntax

const sum = (...values: number[]): number => values.reduce((previous, current) => previous + current);

export const spreadSyntaxTest = new Test("SpreadSyntaxTest")
	.fact("spread_配列リテラルで使ってみる", () => {
		// Arrange
		const items = [1, 2];
		// Act
		const actual = [0, ...items, 3];

		// Assert
		Assert.equal([0, 1, 2, 3], actual);
	})
	.fact("spread_配列リテラルで配列を連結する", () => {
		// Arrange
		const items1 = [0, 1];
		const items2 = [2, 3];
		// Act
		const actual = [...items1, ...items2];

		// Assert
		Assert.equal([0, 1, 2, 3], actual);
	})
	.fact("spread_関数の可変長配列の引数で使ってみる", () => {
		// Arrange
		// Act
		const actual = sum(1, 2, 3);

		// Assert
		Assert.equal(6, actual);
	})
	.fact("spread_関数の可変長配列の引数で使ってみるその2", () => {
		// Arrange
		// Act
		const values = [1, 2, 3];
		const actual = sum(...values);

		// Assert
		Assert.equal(6, actual);
	})
	.fact("spread_配列リテラルに関数の戻り値を指定する", () => {
		// Arrange
		const items = () => [1, 2];
		// Act
		const actual = [0, ...items(), ...items()];

		// Assert
		Assert.equal([0, 1, 2, 1, 2], actual);
	});
