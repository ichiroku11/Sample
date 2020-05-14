import { Assert, Test } from "../unittestlib";

// Error
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Global_Objects/Error


class SampleError extends Error {
	constructor() {
		super();
		this.name = "SampleError";
	}
}

export const errorTest = new Test("ErrorTest")
	.fact("name_Errorインスタンスのnameは\"Error\"", () => {
		Assert.equal("Error", new Error().name);
	})
	.fact("name_カスタムErrorインスタンスのnameはコンストラクタで指定した値", () => {
		Assert.equal("SampleError", new SampleError().name);
	});
