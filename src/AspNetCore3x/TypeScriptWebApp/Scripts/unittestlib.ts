// xUnit.netを参考にしたユニットテスト

class AssertError extends Error {
}

export class Assert {
	public static equal<T>(expected: T, actual: T): void {
		if (expected === actual) {
			return;
		}
		throw new AssertError();
	}
}

class ResultHelper {
	private readonly _container: HTMLElement;

	constructor(selector = ".test-container") {
		const container = document.querySelector(selector);

		let list = container.querySelector("ul");
		if (!list) {
			list = document.createElement("ul");
			container.appendChild(list);
		}

		this._container = list;
	}

	public add(description: string, failed: boolean) {
		let result = document.createElement("li");
		result.classList.add("test-result", failed ? "test-failed" : "test-done");
		result.innerHTML = description;
		this._container.appendChild(result);
	}
}

type TestCase = {
	displayName: string,
	test: () => void,
};

export class Test {
	private readonly _testCases: TestCase[] = [];

	public fact(displayName: string, test: () => void): this {
		this._testCases.push({ displayName, test });
		return this;
	}

	public run(): void {
		const resultHelper = new ResultHelper();

		for (let { displayName, test } of this._testCases) {
			let failed = false;
			try {
				test();
			} catch (ex) {
				failed = true;
			} finally {
				resultHelper.add(displayName, failed);
			}
		}
	}
}

