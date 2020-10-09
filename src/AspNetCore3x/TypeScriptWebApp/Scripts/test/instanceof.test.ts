import { Assert, Test } from "../unittestlib";

// instanceof
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Operators/instanceof

class Animal {}
class Bird extends Animal {}
class Crow extends Bird {}

class TestError extends Error {}

export const instanceofTest = new Test("InstanceOfTest")
	.fact("instanceof_特定のクラスのインスタンスかどうか判定できる", async () => {
		// Arrange
		// Act
		// Assert

		// instanceofは継承も考慮して特定のクラスに属しているか（特定のクラスのサブクラスか）を判定できる

		const crow = new Crow();
		// crowは
		Assert.true(crow instanceof Crow);	// Crowのインスタンスである
		Assert.true(crow instanceof Bird);	// Birdのサブクラスのインスタンスである
		Assert.true(crow instanceof Animal);	// Animalのサブクラスのインスタンスである
		Assert.true(crow instanceof Object);	// Objectのサブクラスのインスタンスである

		const bird = new Bird();
		// birdは
		Assert.false(bird instanceof Crow);	// Crowのインスタンスではない
		Assert.true(bird instanceof Bird);	// Birdのサブクラスのインスタンスである
	})
	.fact("instanceof_Errorクラスのインスタンスかどうか判定できる（念のため）", () => {
		// Arrange
		// Act
		// Assert
		const error = new TestError();

		// これはもちろんtrue
		Assert.true(error instanceof TestError);
		// 親クラスでもtrueになる
		Assert.true(error instanceof Error);
	});
