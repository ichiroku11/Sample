import { Assert, Test } from "../unittestlib";

// 分割代入
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Operators/Destructuring_assignment

export const destructuringAssignmentTest = new Test("DestructuringAssignmentTest")
	.fact("destructuring_配列の分割代入", () => {
		// Arrange
		// Act
		const [a, b, ...rest] = [1, 2, 3, 4, 5];

		// Assert
		Assert.equal(1, a);
		Assert.equal(2, b);
		Assert.equal([3, 4, 5], rest);
	})
	.fact("destructuring_オブジェクトの分割代入", () => {
		// Arrange
		// Act
		const { a, b, ...rest } = { a: 1, b: 2, x: "x", y: "y" };

		// Assert
		Assert.equal(1, a);
		Assert.equal(2, b);
		Assert.equal("x", rest.x);
		Assert.equal("y", rest.y);
	})
	.fact("destructuring_オブジェクトの分割代入で異なる変数名へ代入する", () => {
		// Arrange
		// Act
		const { x: a, y: b } = { x: 1, y: 2 };

		// Assert
		Assert.equal(1, a);
		Assert.equal(2, b);
	});
