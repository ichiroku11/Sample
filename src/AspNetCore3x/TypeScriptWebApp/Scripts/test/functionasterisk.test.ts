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
		let actual: number[] = [];
		for (const value of sample()) {
			actual.push(value);
		}

		// Assert
		Assert.equal([1, 2, 3], actual);
	})
