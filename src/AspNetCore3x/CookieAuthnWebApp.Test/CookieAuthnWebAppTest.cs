using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CookieAuthnWebApp.Test {
	public class CookieAuthnWebAppTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable {
		private readonly WebApplicationFactory<Startup> _factory;

		public CookieAuthnWebAppTest(WebApplicationFactory<Startup> factory) {
			_factory = factory;
		}

		public void Dispose() {
			_factory.Dispose();
		}

		[Fact]
		public async Task GetChallenge_AccountLoginへのリダイレクト() {
			// Arrange
			var options = new WebApplicationFactoryClientOptions {
				AllowAutoRedirect = false,
			};
			var client = _factory.CreateClient(options);

			// Act
			var response = await client.GetAsync("/challenge");

			// Assert
			Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
			Assert.Equal(
				new Uri(_factory.Server.BaseAddress, "/account/login").ToString(),
				response.Headers.Location.GetLeftPart(UriPartial.Path));
		}

		[Fact]
		public async Task GetForbid_AccountAccessDeniedへのリダイレクト() {
			// Arrange
			var options = new WebApplicationFactoryClientOptions {
				AllowAutoRedirect = false,
			};
			var client = _factory.CreateClient(options);

			// Act
			var response = await client.GetAsync("/forbid");

			// Assert
			Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
			Assert.Equal(
				new Uri(_factory.Server.BaseAddress, "/account/accessdenied").ToString(),
				response.Headers.Location.GetLeftPart(UriPartial.Path));
		}

		private class AuthenticateResult {
			public bool Succeeded { get; set; }
			public string NameIdentifier { get; set; }
			public string Role { get; set; }
		}

		[Fact]
		public async Task GetAuthenticate_サインインしていない状態で正しい結果を取得できる() {
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync("/authenticate");
			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<AuthenticateResult>(content);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.False(result.Succeeded);
			Assert.Null(result.NameIdentifier);
			Assert.Null(result.Role);
		}

		[Fact]
		public async Task GetSignin_レスポンスにSetCookieヘッダが含まれる() {
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync("/signin");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.True(response.Headers.Contains("Set-Cookie"));
			// todo: クッキーの中身を確認したいところ
		}

		[Fact]
		public async Task GetSignout_レスポンスにSetCookieヘッダが含まれる() {
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync("/signout");

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.True(response.Headers.Contains("Set-Cookie"));
			// todo: クッキーの中身を確認したいところ
		}

		// todo:
		/*
		[Fact]
		public async Task GetAuthenticate_サインインした状態で正しい結果を取得できる() {
			// Arrange
			var client = _factory.CreateClient();

			// Act
			(await client.GetAsync("/signin")).EnsureSuccessStatusCode();

			var response = await client.GetAsync("/authenticate");
			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<AuthenticateResult>(content);

			// Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.True(result.Succeeded);
			Assert.Equal("1", result.NameIdentifier);
			Assert.Equal("Admin", result.Role);
		}
		*/
	}
}
