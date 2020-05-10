import { assertTest } from "./test/unittestlib.test";
import { arrayTest } from "./test/array.test";
import { destructuringAssignmentTest } from "./test/destructuring-assignment.test";
import { functionAsteriskTest } from "./test/function-asterisk.test";
import { spreadSyntaxTest } from "./test/spread-syntax.test";
import { templateStringsTest } from "./test/template-strings.test";

document.addEventListener("DOMContentLoaded", _ => {
	const tests = [
		assertTest,
		arrayTest,
		destructuringAssignmentTest,
		functionAsteriskTest,
		spreadSyntaxTest,
		templateStringsTest,
	];
	tests.forEach(test => test.run());
});
