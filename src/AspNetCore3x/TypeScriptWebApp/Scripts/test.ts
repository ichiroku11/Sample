import { unitTestLibTest } from "./test/unittestlib.test";
import { arrayTest } from "./test/array.test";

document.addEventListener("DOMContentLoaded", _ => {
	const tests = [
		unitTestLibTest,
		arrayTest
	];
	tests.forEach(test => test.run());
});
