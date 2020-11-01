import { Assert, Test } from "../unittestlib";


export const setTest = new Test("SetTest")
	.fact("add_重複した要素は追加されないし追加しても例外にならない", () => {
		// Arrange
		const actual = new Set<number>();

		// Act
		actual
			.add(1)
			.add(1);

		// Assert
		Assert.equal([1], Array.from(actual));
	})
