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
	});
