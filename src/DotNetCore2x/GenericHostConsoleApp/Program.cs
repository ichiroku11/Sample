using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostConsoleApp {
	class SampleService : IHostedService {
		private readonly ILogger _logger;

		public SampleService(ILogger<SampleService> logger) {
			_logger = logger;
		}

		public Task StartAsync(CancellationToken cancellationToken) {
			_logger.LogInformation(nameof(StartAsync));

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			_logger.LogInformation(nameof(StopAsync));

			return Task.CompletedTask;
		}
	}

	class Program {
		static async Task Main(string[] args) {
			Console.WriteLine("Hello World!");

			// https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/host/generic-host
			await new HostBuilder()
				.ConfigureAppConfiguration((context, config) => {
					// todo: json
					// todo: WebHost.cs
					/*
					var env = hostingContext.HostingEnvironment;

					config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
						  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
					*/
				})
				.ConfigureServices(services => {
					// todo:
					services.AddHostedService<SampleService>();
				})
				.ConfigureLogging((context, logging) => {
					// todo: WebHost.cs
					logging.AddConfiguration(context.Configuration.GetSection("Logging"));
					logging.AddConsole();
					logging.AddDebug();
					//logging.AddEventSourceLogger();
				})
				.RunConsoleAsync();
		}
	}
}
