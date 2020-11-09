import { Assert, Test } from "../unittestlib";

// Set
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Global_Objects/Set

export const setTest = new Test("SetTest")
	.fact("constructor_引数に配列を渡せる", () => {
		// Arrange
		// Act
		const set = new Set([1, 2]);

		// Assert
		Assert.equal([1, 2], Array.from(set));
	})
	.fact("constructor_引数に重複した要素を持つ配列を渡しても例外にならない", () => {
		// Arrange
		// Act
		const set = new Set([1, 1]);

		// Assert
		Assert.equal([1], Array.from(set));
	})
	.fact("constructor_挿入順に列挙できる", () => {
		// Arrange
		// Act
		const set = new Set([3, 2, 1]);

		// Assert
		Assert.equal([3, 2, 1], Array.from(set));
	})
	.fact("add_重複した要素は追加されないし追加しても例外にならない", () => {
		// Arrange
		const set = new Set<number>();

		// Act
		set
			.add(1)
			.add(1);

		// Assert
		Assert.equal([1], Array.from(set));
	})
	.fact("values_挿入順に列挙できる", () => {
		// Arrange
		const set = new Set([3, 2, 1]);

		// Act
		const values = set.values();

		// Assert
		Assert.equal(3, values.next().value);
		Assert.equal(2, values.next().value);
		Assert.equal(1, values.next().value);
	})
	.fact("values_追加や削除をしても挿入順に列挙できる", () => {
		// Arrange
		const set = new Set([3, 2]);
		set.add(1);
		set.delete(2);

		// Act
		const values = set.values();

		// Assert
		Assert.equal(3, values.next().value);
		Assert.equal(1, values.next().value);
	});
