import { Assert, fact } from "./unittest";

fact("テストは成功する", () => {
	// Arrange
	// Act
	// Assert
	Assert.equal("abc", "abc");
});

fact("テストは失敗する", () => {
	// Arrange
	// Act
	// Assert
	Assert.equal("aby", "xyz");
});
