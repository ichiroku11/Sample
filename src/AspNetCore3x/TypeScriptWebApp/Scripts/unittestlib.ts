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

	public static fail(): void {
		Assert.true(false);
	}

	public static notNull<T>(value: T): void {
		if (value === null) {
			throw new AssertError();
		}
	}

	public static null<T>(value: T): void {
		if (value !== null) {
			throw new AssertError();
		}
	}
}

class ResultHelper {
	// モジュールをdetails、summaryを使って表す
	private readonly _details: HTMLDetailsElement;
	// テストケースを格納するul
	private readonly _ul: HTMLElement;

	constructor(moduleName: string, selector = ".test-container") {
		const container = document.querySelector(selector);
		if (!container) {
			throw new Error("container is null");
		}

		const details = document.createElement("details");

		const summary = document.createElement("summary");
		summary.innerText = moduleName;
		details.appendChild(summary);

		const ul = document.createElement("ul");
		details.appendChild(ul);

		container.appendChild(details);

		this._details = details;
		this._ul = ul;
	}

	public add(description: string, failed: boolean) {
		const result = document.createElement("li");
		result.classList.add(failed ? "test-failed" : "test-done");
		result.innerHTML = description;
		this._ul.appendChild(result);

		if (failed) {
			this._details.setAttribute("open", "");
			this._details.classList.remove("test-done");
			this._details.classList.add("test-failed");
		} else {
			if (!this._details.classList.contains("test-failed")) {
				this._details.classList.add("test-done");
			}
		}
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
		const resultHelper = new ResultHelper(this._moduleName);

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
				resultHelper.add(`${testName}`, failed);
			}
		}
	}
}
