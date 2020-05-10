import { Assert, Test } from "../unittestlib";

// テンプレート文字列
// https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/template_strings

export const templateStringsTest = new Test("TemplateStringsTest")
	.fact("templateStrings_改行をそのまま表現できる", () => {
		// Arrange
		// Act
		const actual = `1
2`;
		const expected = "1\n2";

		// Assert
		Assert.equal(expected, actual);
	})
	.fact("templateStrings_変数を内挿できる", () => {
		// Arrange
		// Act
		const value = 1;
		const actual = `0${value}2`;
		const expected = "012";

		// Assert
		Assert.equal(expected, actual);
	});
