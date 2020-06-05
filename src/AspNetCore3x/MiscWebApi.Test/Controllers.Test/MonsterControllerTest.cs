using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using MiscWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

		[Fact]
		public async Task GetAsync_Ok() {
			// Arrange

			// Act
			using var response = await _client.GetAsync("/api/monster");
			var json = await response.Content.ReadAsStringAsync();

			var monsters = JsonSerializer.Deserialize<IList<Monster>>(json, _jsonSerializerOptions);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(2, monsters.Count);
			Assert.Contains(monsters, monster => monster.Id == 1 && monster.Name == "スライム");
			Assert.Contains(monsters, monster => monster.Id == 2 && monster.Name == "ドラキー");
		}

		[Fact]
		public async Task GetByIdAsync_Ok() {
			// Arrange

			// Act
			using var response = await _client.GetAsync("/api/monster/1");

			var json = await response.Content.ReadAsStringAsync();
			var monster = JsonSerializer.Deserialize<Monster>(json, _jsonSerializerOptions);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(1, monster.Id);
			Assert.Equal("スライム", monster.Name);
		}

		[Fact]
		public async Task GetByIdAsync_NotFound() {
			// Arrange
			// Act
			using var response = await _client.GetAsync("/api/monster/0");

			// エラーの場合は、ProblemDetails型（RFC7807）のJSONが返ってくる
			var json = await response.Content.ReadAsStringAsync();
			var problem = JsonSerializer.Deserialize<ProblemDetails>(json, _jsonSerializerOptions);

			// Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
			Assert.NotNull(problem);
			Assert.Equal((int)HttpStatusCode.NotFound, problem.Status.Value);
		}

		[Fact]
		public async Task PostFromAsync_Ok() {
			// Arrange

			// Act
			var formValues = new Dictionary<string, string> {
				{ "id", "1" },
				{ "name", "スライム" },
			};
			using var content = new FormUrlEncodedContent(formValues);
			using var response = await _client.PostAsync("/api/monster/form", content);

			var json = await response.Content.ReadAsStringAsync();
			var monster = JsonSerializer.Deserialize<Monster>(json, _jsonSerializerOptions);

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
			var requestJson = JsonSerializer.Serialize(requestMonster, _jsonSerializerOptions);

			// Act
			using var content = new StringContent(requestJson);
			// Consumes属性がない場合、リクエストヘッダにContentTypeが必要
			content.Headers.ContentType.MediaType = "application/json";
			using var response = await _client.PostAsync("/api/monster/body", content);

			var responseJson = await response.Content.ReadAsStringAsync();
			var responseMonster = JsonSerializer.Deserialize<Monster>(responseJson, _jsonSerializerOptions);

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
			var requestJson = JsonSerializer.Serialize(monster, _jsonSerializerOptions);

			// Act
			using var content = new StringContent(requestJson);
			using var response = await _client.PostAsync("/api/monster/body", content);

			var responseJson = await response.Content.ReadAsStringAsync();
			var problem = JsonSerializer.Deserialize<ProblemDetails>(responseJson, _jsonSerializerOptions);

			// Assert
			Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
			Assert.NotNull(problem);
			Assert.Equal((int)HttpStatusCode.UnsupportedMediaType, problem.Status.Value);
		}
	}
}
