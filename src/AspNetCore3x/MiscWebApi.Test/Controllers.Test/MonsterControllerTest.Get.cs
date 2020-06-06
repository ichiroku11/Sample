using Microsoft.AspNetCore.Mvc;
using MiscWebApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiscWebApi.Test.Controllers.Test {
	public partial class MonsterControllerTest {
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
	}
}
