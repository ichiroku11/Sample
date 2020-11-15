import { Assert, Test } from "../unittestlib";

// Map
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Global_Objects/Map

export const mapTest = new Test("MapTest")
	.fact("constructor_引数に[key, value]の配列を渡せる", () => {
		// Arrange
		// Act
		const map = new Map([[1, "x"], [2, "y"]]);

		// Assert
		Assert.equal(2, map.size);
		Assert.equal("x", map.get(1));
		Assert.equal("y", map.get(2));
	})
	.fact("constructor_引数に重複した要素を渡すと上書きされる", () => {
		// Arrange
		// Act
		const map = new Map([[1, "x"], [1, "y"]]);

		// Assert
		Assert.equal(1, map.size);
		Assert.equal("y", map.get(1));
	})
	.fact("entries_挿入順に取得できる", () => {
		// Arrange
		const map = new Map<number, string>();
		map.set(1, "x").set(2, "y");

		// Act
		const keys: number[] = [];
		const values: string[] = [];
		for (const [key, value] of map.entries()) {
			keys.push(key);
			values.push(value);
		}

		// Assert
		Assert.equal([1, 2], keys);
		Assert.equal(["x", "y"], values);
	})
	.fact("forEach_列挙できる", () => {
		// Arrange
		const map = new Map([[1, "x"]]);

		// Act
		// Assert
		Assert.equal(1, map.size);
		map.forEach((value, key) => {
			Assert.equal(key, 1);
			Assert.equal(value, "x");
		});
	})
	.fact("forof_列挙できる", () => {
		// Arrange
		const map = new Map([[1, "x"]]);

		// Act
		// Assert
		Assert.equal(1, map.size);
		for (const [key, value] of map) {
			Assert.equal(key, 1);
			Assert.equal(value, "x");
		}
	});
