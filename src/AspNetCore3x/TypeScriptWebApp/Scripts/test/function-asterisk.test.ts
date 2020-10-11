import { Assert, Test } from "../unittestlib";

// ジェネレーター関数
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Operators/function*

function* sample() {
	yield 1;
	yield 2;
	yield 3;
}

export const functionAsteriskTest = new Test("FunctionAsteriskTest")
	.fact("function*_使ってみる", () => {
		// Arrange
		// Act
		const generator = sample();

		// actualの型
		// actual: (number | void)[]
		const actual = [
			generator.next().value,
			generator.next().value,
			generator.next().value,
		];

		// Assert
		Assert.equal([1, 2, 3], actual);
	})
	.fact("function*_forof文で使ってみる", () => {
		// for...of
		// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Statements/for...of
		// for...in
		// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Statements/for...in
		// 注: for...in はインデックスの順序が重要となる 配列 の繰り返しには使うべきではありません

		// Arrange
		// Act
		const actual: number[] = [];
		for (const value of sample()) {
			actual.push(value);
		}

		// Assert
		Assert.equal([1, 2, 3], actual);
	})
