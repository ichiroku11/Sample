using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest.DependencyInjection {
	// 参考
	// https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
	public class ServiceCollectionTest {
		private class SampleService {
			public SampleService() : this(Guid.NewGuid()) {
			}

			public SampleService(Guid value) {
				Value = value;
			}

			public Guid Value { get; }
		}


		private readonly ITestOutputHelper _output;

		public ServiceCollectionTest(ITestOutputHelper output) {
			_output = output;
		}


		[Fact]
		public void AddTransient_() {
			// Arrange
			var services = new ServiceCollection();
			services.AddTransient<SampleService>();

			var provider = services.BuildServiceProvider();

			// Act
			var service1 = provider.GetRequiredService<SampleService>();
			var service2 = provider.GetRequiredService<SampleService>();

			_output.WriteLine($"{service1.Value}");
			_output.WriteLine($"{service2.Value}");

			// Assert
			Assert.NotEqual(service1.Value, service2.Value);
		}
	}
}
