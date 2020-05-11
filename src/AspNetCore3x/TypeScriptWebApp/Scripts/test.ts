import { assertTest } from "./test/unittestlib.test";
import { arrayTest } from "./test/array.test";
import { asyncAwaitTest } from "./test/async-await.test";
import { destructuringAssignmentTest } from "./test/destructuring-assignment.test";
import { functionAsteriskTest } from "./test/function-asterisk.test";
import { spreadSyntaxTest } from "./test/spread-syntax.test";
import { templateStringsTest } from "./test/template-strings.test";

document.addEventListener("DOMContentLoaded", async () => {
	const tests = [
		assertTest,
		arrayTest,
		asyncAwaitTest,
		destructuringAssignmentTest,
		functionAsteriskTest,
		spreadSyntaxTest,
		templateStringsTest,
	];
	const promises = tests.map(test => test.run());
	await Promise.all(promises);
});
