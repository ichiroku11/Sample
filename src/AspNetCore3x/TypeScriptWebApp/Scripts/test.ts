import { assertTest } from "./test/unittestlib.test";
import { arrayTest } from "./test/array.test";
import { spreadSyntaxTest } from "./test/spreadsyntax.test";

document.addEventListener("DOMContentLoaded", _ => {
	const tests = [
		assertTest,
		arrayTest,
		spreadSyntaxTest,
	];
	tests.forEach(test => test.run());
});
