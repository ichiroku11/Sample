using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	class Program {
		static async Task Main(string[] args) {
			// 参考
			// https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2
			// https://docs.microsoft.com/ja-jp/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/background-tasks-with-ihostedservice

			await new HostBuilder()
				.ConfigureAppConfiguration((context, config) => {
				})
				.ConfigureServices(services => {
					services
						.AddHostedService<ProducerService>()
						.AddHostedService<ConsumerService>();
				})
				.RunConsoleAsync();
		}
	}
}
