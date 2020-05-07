import { assertTest } from "./test/unittestlib.test";
import { arrayTest } from "./test/array.test";
import { functionAsteriskTest } from "./test/functionasterisk.test";
import { spreadSyntaxTest } from "./test/spreadsyntax.test";

document.addEventListener("DOMContentLoaded", _ => {
	const tests = [
		assertTest,
		arrayTest,
		functionAsteriskTest,
		spreadSyntaxTest,
	];
	tests.forEach(test => test.run());
});
