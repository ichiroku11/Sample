import { Assert, Test } from "../unittestlib";

// Fetch
// https://developer.mozilla.org/ja/docs/Web/API/Fetch_API

type Sample = {
	number: number,
	text: string,
};

export const fetchTest = new Test("FetchTest")
	.fact("GETリクエストのサンプル", async () => {
		// GETリクエスト
		const response = await fetch("/api/samplejson");
		Assert.equal(200, response.status);

		// レスポンスからJSONを取り出す
		const json: Sample = await response.json();
		Assert.equal(json.number, 1);
		Assert.equal(json.text, "a");
	})
	.fact("POSTリクエスト（JSON）のサンプル", async () => {
		// JSONデータをPOST
		const response = await fetch("/api/samplejson", {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify({ number: 2, text: "b" }),
		});
		Assert.equal(200, response.status);

		// レスポンスからJSONを取り出す
		const json: Sample = await response.json();
		Assert.equal(json.number, 2);
		Assert.equal(json.text, "b");
	})
	.fact("POSTリクエスト（フォーム）のサンプル", async () => {
		// フォーム
		var param = new URLSearchParams();
		param.append("number", "2");
		param.append("text", "b");

		// フォームのPOST
		const response = await fetch("/api/samplejson/form", {
			method: "POST",
			headers: {
				"Content-Type": "application/x-www-form-urlencoded",
			},
			body: param,
		});
		Assert.equal(200, response.status);

		const json: Sample = await response.json();
		Assert.equal(json.number, 2);
		Assert.equal(json.text, "b");
	});
