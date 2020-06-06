using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using MiscWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MiscWebApi.Test.Controllers.Test {
	public class MonsterControllerTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable {
		private static readonly JsonSerializerOptions _jsonSerializerOptions
			= new JsonSerializerOptions {
				PropertyNameCaseInsensitive = true,
			};

		private static readonly Monster _slime = new Monster {
			Id = 1,
			Name = "スライム",
		};

		private static StringContent GetJsonStringContent<TModel>(TModel model) {
			var json = JsonSerializer.Serialize(model, _jsonSerializerOptions);
			var content = new StringContent(json);
			return content;
		}

		private static async Task<TModel> DeserializeAsync<TModel>(HttpResponseMessage response) {
			var json = await response.Content.ReadAsStringAsync();
			var model = JsonSerializer.Deserialize<TModel>(json, _jsonSerializerOptions);
			return model;
		}

		public enum PostContentType {
			FormUrlEncoded,
			JsonString,
			JsonStringTextPlain,
		}

		private static HttpContent GetContent(PostContentType contentType, Monster monster) {
			switch (contentType) {
				case PostContentType.FormUrlEncoded:
					// application/x-www-form-urlencoded
					var formValues = new Dictionary<string, string> {
						{ "id", monster.Id.ToString() },
						{ "name", monster.Name },
					};
					return new FormUrlEncodedContent(formValues);
				case PostContentType.JsonString:
				case PostContentType.JsonStringTextPlain:
					var content = GetJsonStringContent(monster);
					content.Headers.ContentType.MediaType
						= contentType == PostContentType.JsonString
							? "application/json"
							: "text/plain";
					return content;
			}

			throw new ArgumentOutOfRangeException(nameof(contentType));
		}

		private readonly ITestOutputHelper _output;
		private readonly WebApplicationFactory<Startup> _factory;
		private HttpClient _client;

		public MonsterControllerTest(ITestOutputHelper output, WebApplicationFactory<Startup> factory) {
			_output = output;
			_factory = factory;
			_client = _factory.CreateClient();
		}

		public void Dispose() {
			_client?.Dispose();
			_client = null;
		}

		private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) {
			_output.WriteLine(request.ToString());
			if (request.Content != null) {
				_output.WriteLine(await request.Content.ReadAsStringAsync());
			}

			var response = await _client.SendAsync(request);

			_output.WriteLine(response.ToString());
			if (response.Content != null) {
				_output.WriteLine(await response.Content.ReadAsStringAsync());
			}
			return response;
		}

		[Fact]
		public async Task GetAsync_Ok() {
			// Arrange
			using var request = new HttpRequestMessage(HttpMethod.Get, "/api/monster");

			// Act
			using var response = await SendAsync(request);
			var monsters = await DeserializeAsync<IList<Monster>>(response);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(2, monsters.Count);
			Assert.Contains(monsters, monster => monster.Id == 1 && monster.Name == "スライム");
			Assert.Contains(monsters, monster => monster.Id == 2 && monster.Name == "ドラキー");
		}

		[Fact]
		public async Task GetByIdAsync_Ok() {
			// Arrange
			using var request = new HttpRequestMessage(HttpMethod.Get, "/api/monster/1");

			// Act
			using var response = await SendAsync(request);
			var monster = await DeserializeAsync<Monster>(response);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(1, monster.Id);
			Assert.Equal("スライム", monster.Name);
		}

		[Fact]
		public async Task GetByIdAsync_NotFound() {
			// Arrange
			using var request = new HttpRequestMessage(HttpMethod.Get, "/api/monster/0");

			// Act
			using var response = await SendAsync(request);
			// エラーの場合は、ProblemDetails型（RFC7807）のJSONが返ってくる
			var problem = await DeserializeAsync<ProblemDetails>(response);

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
			Assert.NotNull(problem);
			Assert.Equal((int)HttpStatusCode.NotFound, problem.Status.Value);
		}

		#region FromXxx属性がないPOSTアクション
		// JSONをバインドできる
		[Theory]
		[InlineData(PostContentType.JsonString)]
		public async Task PostAsync_Ok(PostContentType contentType) {
			// Arrange
			using var content = GetContent(contentType, _slime);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var monster = await DeserializeAsync<Monster>(response);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(_slime.Id, monster.Id);
			Assert.Equal(_slime.Name, monster.Name);
		}

		// Formデータをバインドできない
		// Content-Typeがtext/plainのJSONをバインドできない
		[Theory]
		[InlineData(PostContentType.FormUrlEncoded)]
		[InlineData(PostContentType.JsonStringTextPlain)]
		public async Task PostAsync_UnsupportedMediaType(PostContentType contentType) {
			// Arrange
			using var content = GetContent(contentType, _slime);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var problem = await DeserializeAsync<ProblemDetails>(response);

			// Assert
			Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
			Assert.NotNull(problem);
			Assert.Equal((int)HttpStatusCode.UnsupportedMediaType, problem.Status.Value);
		}
		#endregion

		#region FromForm属性に対するPOSTアクション
		// Formデータをバインドできる
		[Theory]
		[InlineData(PostContentType.FormUrlEncoded)]
		public async Task PostFormAsync_Ok(PostContentType contentType) {
			// Arrange
			using var content = GetContent(contentType, _slime);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster/form") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var monster = await DeserializeAsync<Monster>(response);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(_slime.Id, monster.Id);
			Assert.Equal(_slime.Name, monster.Name);
		}

		// JSONをバインドできない（400が返ってくる）
		[Theory]
		[InlineData(PostContentType.JsonString)]
		public async Task PostFormAsync_BadRequest(PostContentType contentType) {
			// Arrange
			using var content = GetContent(contentType, _slime);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster/form") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var problem = await DeserializeAsync<ProblemDetails>(response);

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			Assert.NotNull(problem);
			Assert.Equal((int)HttpStatusCode.BadRequest, problem.Status.Value);
		}
		#endregion

		#region FromBody属性に対するPOSTアクション
		// Consumes属性がない場合
		// JSONをバインドできる
		[Theory]
		[InlineData(PostContentType.JsonString)]
		public async Task PostBodyAsync_Ok(PostContentType contentType) {
			// Arrange
			using var content = GetContent(contentType, _slime);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster/body") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var responseMonster = await DeserializeAsync<Monster>(response);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(_slime.Id, responseMonster.Id);
			Assert.Equal(_slime.Name, responseMonster.Name);
		}

		// Consumes属性がない場合
		// Formデータをバインドできない（415）
		// Content-Typeがtext/plainのJSONをバインドできない（415）
		// レスポンスはProblemDetailsのJSON
		[Theory]
		[InlineData(PostContentType.FormUrlEncoded)]
		[InlineData(PostContentType.JsonStringTextPlain)]
		public async Task PostBodyAsync_UnsupportedMediaType(PostContentType contentType) {
			// Arrange
			using var content = GetContent(contentType, _slime);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster/body") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var problem = await DeserializeAsync<ProblemDetails>(response);

			// Assert
			Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
			Assert.NotNull(problem);
			Assert.Equal((int)HttpStatusCode.UnsupportedMediaType, problem.Status.Value);
		}

		// Consumes属性がある場合
		// JSONをバインドできる
		[Theory]
		[InlineData(PostContentType.JsonString)]
		public async Task PostBodyJsonAsync_Ok(PostContentType contentType) {
			// Arrange
			using var content = GetContent(contentType, _slime);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster/body/json") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var responseMonster = await DeserializeAsync<Monster>(response);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(_slime.Id, responseMonster.Id);
			Assert.Equal(_slime.Name, responseMonster.Name);
		}

		// Consumes属性がある場合
		// Formデータをバインドできない（415）
		// Content-Typeがtext/plainのJSONをバインドできない（415）
		// レスポンスは空っぽ
		[Theory]
		[InlineData(PostContentType.FormUrlEncoded)]
		[InlineData(PostContentType.JsonStringTextPlain)]
		public async Task PostBodyJsonAsync_UnsupportedMediaType(PostContentType contentType) {
			// Arrange
			using var content = GetContent(contentType, _slime);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster/body/json") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var responseText = await response.Content?.ReadAsStringAsync();

			// Assert
			Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
			Assert.NotNull(response.Content);
			Assert.NotNull(responseText);
			Assert.Equal(0, responseText.Length);
		}
		#endregion
	}
}
