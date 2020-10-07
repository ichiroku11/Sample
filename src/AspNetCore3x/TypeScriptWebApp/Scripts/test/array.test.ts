import { Assert, Test } from "../unittestlib";

// Array.fromを使ってEnumerable.Rangeのようなメソッドを作る
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Global_Objects/Array/from
const range = (start: number, count: number) => Array.from({ length: count }, (_, index) => start + index);

export const arrayTest = new Test("ArrayTest")
	.fact("filter_試す", () => {
		// Arrange
		// Act
		const actual = [1, 2, 3, 4].filter(item => item % 2 === 0);

		// Assert
		Assert.equal([2, 4], actual);
	})
	.fact("from_rangeメソッドを作る", () => {
		// Arrange
		// Act
		const actual = range(1, 3);

		// Assert
		Assert.equal([1, 2, 3], actual);
	})
	.fact("map_試す", () => {
		// Arrange
		// Act
		const actual = [1, 2, 3].map(item => item + 1);

		// Assert
		Assert.equal([2, 3, 4], actual);
	})
	.fact("reduce_試す", () => {
		// Arrange
		// Act
		const actual = [1, 2, 3].reduce((previousValue, currentValue) => previousValue + currentValue);

		// Assert
		Assert.equal(6, actual);
	});
