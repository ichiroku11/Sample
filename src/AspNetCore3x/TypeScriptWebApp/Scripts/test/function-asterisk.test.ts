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
		// Arrange
		// Act
		const actual: number[] = [];
		for (const value of sample()) {
			actual.push(value);
		}

		// Assert
		Assert.equal([1, 2, 3], actual);
	})
