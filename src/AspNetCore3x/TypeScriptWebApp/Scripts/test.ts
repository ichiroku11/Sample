import { assertTest } from "./test/unittestlib.test";
import { arrayTest } from "./test/array.test";
import { asyncAwaitTest } from "./test/async-await.test";
import { destructuringAssignmentTest } from "./test/destructuring-assignment.test";
import { errorTest } from "./test/error.test";
import { fetchTest } from "./test/fetch.test";
import { functionAsteriskTest } from "./test/function-asterisk.test";
import { instanceofTest } from "./test/instanceof.test";
import { mapTest } from "./test/map.test"
import { promiseTest } from "./test/promise.test";
import { rxjsCreatingOperatorsTest } from "./test/rxjs.test"
import { setTest } from "./test/set.test"
import { spreadSyntaxTest } from "./test/spread-syntax.test";
import { templateStringsTest } from "./test/template-strings.test";
import { urlSearchParamsTest } from "./test/url.test";

document.addEventListener("DOMContentLoaded", async () => {
	const tests = [
		assertTest,
		arrayTest,
		asyncAwaitTest,
		destructuringAssignmentTest,
		errorTest,
		fetchTest,
		functionAsteriskTest,
		instanceofTest,
		mapTest,
		promiseTest,
		rxjsCreatingOperatorsTest,
		setTest,
		spreadSyntaxTest,
		templateStringsTest,
		urlSearchParamsTest,
	];
	const promises = tests.map(test => test.run());
	await Promise.all(promises);
});
