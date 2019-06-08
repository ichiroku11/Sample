using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	// Producer-Consumerパターンを作ってみる
	// 参考
	// https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2
	// https://docs.microsoft.com/ja-jp/dotnet/standard/microservices-architecture/multi-container-microservice-net-applications/background-tasks-with-ihostedservice
	class Program {
		static async Task Main(string[] args) {
			await new HostBuilder()
				.ConfigureAppConfiguration((context, config) => {
				})
				.ConfigureServices(services => {
					services
						.AddSingleton<IBlockingQueue<IEnumerable<byte>>, BlockingQueue<IEnumerable<byte>>>()
						.AddSingleton<IConsoleHelper, ConsoleHelper>()
						.AddHostedService<ProducerService>()
						.AddHostedService<ConsumerService>();
				})
				.RunConsoleAsync();
		}
	}
}
