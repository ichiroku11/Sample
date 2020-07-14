import { Assert, Test } from "../unittestlib";

export const urlSearchParamsTest = new Test("URLSearchParamsTest")
	.fact("toString_クエリ文字列を取得できる", () => {
		// Arrange
		const params = new URLSearchParams();
		params.append("x", "1");
		params.append("y", "2");

		// Act
		// Assert
		Assert.equal("x=1&y=2", params.toString());
	})
	.fact("get_appendした値を取得できる", () => {
		// Arrange
		const params = new URLSearchParams();
		params.append("x", "1");
		params.append("y", "2");

		// Act
		// Assert
		Assert.equal("1", params.get("x"));
		Assert.equal("2", params.get("y"));
	})
	.fact("get_存在しないキーはnullを返す", () => {
		// Arrange
		const params = new URLSearchParams();

		// Act
		// Assert
		Assert.false(params.has("z"));
		Assert.null(params.get("z"));
	});
