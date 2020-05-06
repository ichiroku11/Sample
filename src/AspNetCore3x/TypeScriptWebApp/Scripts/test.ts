import { assertTest } from "./test/unittestlib.test";
import { arrayTest } from "./test/array.test";

document.addEventListener("DOMContentLoaded", _ => {
	const tests = [
		assertTest,
		arrayTest
	];
	tests.forEach(test => test.run());
});
