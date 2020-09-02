using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace MiscWebApp.Test {
	public abstract class TestBase : IClassFixture<WebApplicationFactory<Startup>>, IDisposable {
		private readonly WebApplicationFactory<Startup> _factory;

		protected TestBase(ITestOutputHelper output, WebApplicationFactory<Startup> factory) {
			Output = output;

			_factory = factory;
			Client = _factory.CreateClient();
		}

		protected ITestOutputHelper Output { get; }
		protected HttpClient Client { get; private set; }

		public void Dispose() {
			Client?.Dispose();
			Client = null;
		}
	}
}
