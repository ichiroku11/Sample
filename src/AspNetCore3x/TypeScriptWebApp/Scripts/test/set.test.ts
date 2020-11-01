import { Assert, Test } from "../unittestlib";


export const setTest = new Test("SetTest")
	.fact("constructor_引数に配列を渡せる", () => {
		// Arrange
		// Act
		const actual = new Set([1, 2]);

		// Assert
		Assert.equal([1, 2], Array.from(actual));
	})
	.fact("add_重複した要素は追加されないし追加しても例外にならない", () => {
		// Arrange
		const actual = new Set<number>();

		// Act
		actual
			.add(1)
			.add(1);

		// Assert
		Assert.equal([1], Array.from(actual));
	});
