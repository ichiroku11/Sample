import { Assert, Test } from "../unittestlib";

// Fetch
// https://developer.mozilla.org/ja/docs/Web/API/Fetch_API

const baseUrl = "/api/samplejson";
type Sample = { number: number, text: string };

export const fetchTest = new Test("FetchTest")
	.fact("GETリクエストのサンプル", async () => {
		// GETリクエスト
		const response = await fetch(baseUrl);
		Assert.equal(200, response.status);

		// レスポンスからJSONを取り出す
		const json: Sample = await response.json();
		Assert.equal(json.number, 1);
		Assert.equal(json.text, "a");
	})
	.fact("POSTリクエスト（JSON）のサンプル", async () => {
		// POSTリクエスト
		const response = await fetch(baseUrl, {
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
