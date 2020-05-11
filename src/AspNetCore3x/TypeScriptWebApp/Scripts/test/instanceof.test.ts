import { Assert, Test } from "../unittestlib";

// instanceof
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Operators/instanceof

class Animal {}
class Bird extends Animal {}
class Crow extends Bird {}

export const instanceofTest = new Test("instanceofTest")
	.fact("instanceof_試す", async () => {
		// Arrange
		// Act
		// Asseert
		// todo:
	});
