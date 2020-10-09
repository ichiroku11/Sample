import { Assert, Test } from "../unittestlib";
import { empty } from "rxjs";

export const rxjsCreatingOperatorsTest = new Test("RxJS.CreatingOperatorsTest")
	.fact("empty_completeだけが呼ばれる", () => {
		// Arrange
		let completed = false;

		// Act
		empty().subscribe(
			_ => Assert.fail(),
			_ => Assert.fail(),
			// completeだけが呼ばれる
			() => {
				Assert.false(completed);
				completed = true;
			});

		Assert.true(completed);
	});

