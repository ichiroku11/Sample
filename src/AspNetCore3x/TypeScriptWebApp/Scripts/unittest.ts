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


export function fact(description: string, test: () => void): void {
	let failed = false;
	try {
		test();
	} catch (ex) {
		failed = true;
	} finally {
		// 仮
		if (failed) {
			console.error(description);
		} else {
			console.info(description);
		}
	}
}
