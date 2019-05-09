using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostConsoleApp {
	class Program {
		static async Task Main(string[] args) {
			Console.WriteLine("Hello World!");

			// https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/host/generic-host
			await new HostBuilder()
				.ConfigureAppConfiguration((context, config) => {
					// appsettings.jsonを読み込む設定
					/*
					var env = hostingContext.HostingEnvironment;

					// Install-Package Microsoft.Extensions.Configuration.Json
					config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
						  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
					*/
				})
				.ConfigureServices(services => {
					// 実行するホステッドサービスを追加
					services.AddHostedService<SampleService>();
				})
				.ConfigureLogging((context, logging) => {
					// ログの構成
					logging.AddConfiguration(context.Configuration.GetSection("Logging"));

					// Install-Package Microsoft.Extensions.Logging.Console
					logging.AddConsole();

					// Install-Package Microsoft.Extensions.Logging.Debug
					logging.AddDebug();

					//logging.AddEventSourceLogger();
				})
				.RunConsoleAsync();
		}
	}
}
