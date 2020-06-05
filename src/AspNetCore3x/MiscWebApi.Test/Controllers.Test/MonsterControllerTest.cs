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

			var response = await _client.SendAsync(request);

			_output.WriteLine(response.ToString());
			return response;
		}

		private StringContent GetJsonStringContent<TModel>(TModel model) {
			var json = JsonSerializer.Serialize(model, _jsonSerializerOptions);
			var content = new StringContent(json);
			return content;
		}

		private async Task<TModel> DeserializeAsync<TModel>(HttpResponseMessage response) {
			var json = await response.Content.ReadAsStringAsync();
			var model = JsonSerializer.Deserialize<TModel>(json, _jsonSerializerOptions);
			return model;
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

		[Fact]
		public async Task PostFromAsync_Ok() {
			// Arrange
			var formValues = new Dictionary<string, string> {
				{ "id", "1" },
				{ "name", "スライム" },
			};
			using var content = new FormUrlEncodedContent(formValues);
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster/form") {
				Content = content,
			};

			// Act
			using var response = await _client.SendAsync(request);
			var monster = await DeserializeAsync<Monster>(response);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(1, monster.Id);
			Assert.Equal("スライム", monster.Name);
		}

		[Fact]
		public async Task PostBodyAsync_Ok() {
			// Arrange
			var requestMonster = new Monster {
				Id = 1,
				Name = "スライム",
			};

			using var content = GetJsonStringContent(requestMonster);
			// Consumes属性がない場合、リクエストヘッダにContentTypeが必要
			content.Headers.ContentType.MediaType = "application/json";
			using var request = new HttpRequestMessage(HttpMethod.Post, "/api/monster/body") {
				Content = content,
			};

			// Act
			using var response = await SendAsync(request);
			var responseMonster = await DeserializeAsync<Monster>(response);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(requestMonster.Id, responseMonster.Id);
			Assert.Equal(requestMonster.Name, responseMonster.Name);
		}

		[Fact]
		public async Task PostBodyAsync_UnsupportedMediaType() {
			// Arrange
			var monster = new Monster {
				Id = 1,
				Name = "スライム",
			};
			using var content = GetJsonStringContent(monster);
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
	}
}
