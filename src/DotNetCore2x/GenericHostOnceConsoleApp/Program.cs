using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GenericHostOnceConsoleApp {
	class Program {
		static async Task Main(string[] args) {
			var builder = new HostBuilder()
				.ConfigureServices(services => {
					services.Configure<ConsoleLifetimeOptions>(config => {
						// メッセージを抑制する
						config.SuppressStatusMessages = true;
					});

					services.AddHostedService<SampleService>();
				})
				.ConfigureLogging((context, logging) => {
					logging.AddConsole();
					logging.AddDebug();
				})
				.UseConsoleLifetime();

			using (var host = builder.Build()) {
				// 開始する
				await host.StartAsync();

				// 終了する
				await host.StopAsync();
			}
		}
	}
}
