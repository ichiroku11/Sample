using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using MiscWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace MiscWebApi.Test.Controllers.Test {
	public class MonsterControllerTest : IClassFixture<WebApplicationFactory<Startup>> {
		private static readonly JsonSerializerOptions _jsonSerializerOptions
			= new JsonSerializerOptions {
				PropertyNameCaseInsensitive = true,
			};

		private readonly WebApplicationFactory<Startup> _factory;

		public MonsterControllerTest(WebApplicationFactory<Startup> factory) {
			_factory = factory;
		}

		[Fact]
		public async Task GetAsync_Ok() {
			// Arrange
			using var client = _factory.CreateClient();

			// Act
			using var response = await client.GetAsync("/api/monster");
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
			using var client = _factory.CreateClient();

			// Act
			using var response = await client.GetAsync("/api/monster/1");

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
			using var client = _factory.CreateClient();

			// Act
			using var response = await client.GetAsync("/api/monster/0");

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
			using var client = _factory.CreateClient();

			// Act
			var formValues = new Dictionary<string, string> {
				{ "id", "1" },
				{ "name", "スライム" },
			};
			using var content = new FormUrlEncodedContent(formValues);
			using var response = await client.PostAsync("/api/monster/form", content);

			var json = await response.Content.ReadAsStringAsync();
			var monster = JsonSerializer.Deserialize<Monster>(json, _jsonSerializerOptions);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal(1, monster.Id);
			Assert.Equal("スライム", monster.Name);
		}
	}
}
