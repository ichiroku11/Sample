import { Assert, Test } from "../unittestlib";

// Promise
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Global_Objects/Promise

// JavaScript Promiseの本
// https://azu.github.io/promises-book/

// todo: 作ってみる
/*
async function delay() {
}
async function timeout() {
}
*/

export const promiseTest = new Test("PromiseTest")
	.fact("promise_resolveはシンタックスシュガー", async () => {
		// Arrange
		// Act
		const value = await Promise.resolve(1);
		// ↑は以下のシンタックスシュガー
		// const value = new Promise(resolve => resolve(1));

		// Assert
		Assert.equal(1, value);
	})
	.fact("promise_rejectはシンタックスシュガー", async () => {
		// Arrange
		// Act
		// Assert

		let error = false;
		try {
			await Promise.reject("error!");
		} catch (e) {
			Assert.equal("error!", e as string);
			error = true;
		}

		Assert.true(error);

		// todo: 例外がスローされる
		// Assert.throwsを作れると
	});
