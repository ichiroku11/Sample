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

export function fact(description: string, test: () => void): void {
	let failed = false;
	try {
		test();
	} catch (ex) {
		failed = true;
	} finally {
		new ResultHelper().add(description, failed);
	}
}
