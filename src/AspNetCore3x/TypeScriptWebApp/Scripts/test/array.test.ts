import { Assert, Test } from "../unittestlib";

export const arrayTest = new Test("ArrayTest")
	.fact("filter_試す", () => {
		// Arrange
		// Act
		const actual = [1, 2, 3, 4].filter(item => item % 2 === 0);

		// Assert
		Assert.equal([2, 4], actual);
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
