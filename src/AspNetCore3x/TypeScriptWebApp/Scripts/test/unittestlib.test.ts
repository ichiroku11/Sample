import { Assert, Test } from "../unittestlib";

export const assertTest = new Test("AssertTest")
	.fact("equal_文字列が等しいと判断できる", () => {
		// Arrange
		// Act
		// Assert
		Assert.equal("abc", "abc");
	})
	.fact("equal_配列が等しいと判断できる", () => {
		// Arrange
		// Act
		// Assert
		Assert.equal([1, 2, 3], [1, 2, 3]);
	})
	.fact("true_判断できる", () => {
		// Arrange
		// Act
		// Assert
		Assert.true(true);
	})
	.fact("false_判断できる", () => {
		// Arrange
		// Act
		// Assert
		Assert.false(false);
	})
	.fact("notNull_判断できる", () => {
		// Arrange
		// Act
		// Assert
		Assert.notNull(undefined);
		Assert.notNull(false);
		Assert.notNull(0);
		Assert.notNull("");
		Assert.notNull({});
		Assert.notNull([]);
	})
	.fact("null_判断できる", () => {
		// Arrange
		// Act
		// Assert
		Assert.null(null);
	});
