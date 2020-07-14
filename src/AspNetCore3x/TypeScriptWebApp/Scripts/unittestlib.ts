// xUnit.netを参考にしたユニットテスト

class AssertError extends Error {
}

export class Assert {
	private static arrayEqual<T>(expected: T[], actual: T[]): void {
		if (expected.length !== actual.length) {
			throw new AssertError();
		}

		const length = expected.length;
		for (let index = 0; index < length; index++) {
			Assert.equal(expected[index], actual[index]);
		}
	}

	public static equal<T>(expected: T, actual: T): void {
		if (Array.isArray(expected) && Array.isArray(actual)) {
			Assert.arrayEqual(expected, actual);
			return;
		}

		if (expected !== actual) {
			throw new AssertError();
		}
	}

	public static true(condition: boolean): void {
		if (!condition) {
			throw new AssertError();
		}
	}

	public static false(condition: boolean): void {
		if (condition) {
			throw new AssertError();
		}
	}

	public static notNull<T>(value: T) {
		if (value === null) {
			throw new AssertError();
		}
	}

	public static null<T>(value: T) {
		if (value !== null) {
			throw new AssertError();
		}
	}
}

class ResultHelper {
	private readonly _container: HTMLElement;

	constructor(selector = ".test-container") {
		const container = document.querySelector(selector);
		if (!container) {
			throw new Error("container is null");
		}

		let list = container.querySelector("ul");
		if (!list) {
			list = document.createElement("ul");
			container.appendChild(list);
		}

		this._container = list;
	}

	public add(description: string, failed: boolean) {
		const result = document.createElement("li");
		result.classList.add("test-result", failed ? "test-failed" : "test-done");
		result.innerHTML = description;
		this._container.appendChild(result);
	}
}

type TestFunc = () => void | Promise<void>;

type TestCase = {
	testName: string;
	testFunc: TestFunc;
};

export class Test {
	private readonly _moduleName: string;
	private readonly _testCases: TestCase[] = [];

	constructor(moduleName: string) {
		this._moduleName = moduleName;
	}

	public fact(testName: string, testFunc: TestFunc): this {
		this._testCases.push({ testName, testFunc });
		return this;
	}

	public async run(): Promise<void> {
		const resultHelper = new ResultHelper();

		for (const { testName, testFunc } of this._testCases) {
			let failed = false;
			try {
				const result = testFunc();
				if (result instanceof Promise) {
					await result;
				}
			} catch (ex) {
				failed = true;
			} finally {
				resultHelper.add(`${this._moduleName}: ${testName}`, failed);
			}
		}
	}
}

