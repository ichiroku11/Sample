import { Assert, Test } from "../unittestlib";

// Fetch
// https://developer.mozilla.org/ja/docs/Web/API/Fetch_API

const baseUrl = "/api/samplejson";
type GetResponse = { number: number, text: string };

export const fetchTest = new Test("FetchTest")
	.fact("GETリクエストのサンプル", async () => {
		const response = await fetch(baseUrl);
		Assert.equal(200, response.status);

		// レスポンスからJSONを取り出す
		const json: GetResponse = await response.json();
		Assert.equal(json.number, 1);
		Assert.equal(json.text, "a");
	});
