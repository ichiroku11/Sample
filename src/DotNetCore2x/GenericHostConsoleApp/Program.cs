using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace GenericHostConsoleApp {
	class Program {
		static async Task Main(string[] args) {
			Console.WriteLine("Hello World!");

			// https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/host/generic-host
			await new HostBuilder()
				.ConfigureAppConfiguration((context, config) => {
					// todo: json
				})
				.ConfigureServices(services => {
					// todo:
				})
				.ConfigureLogging((context, logging) => {
					// todo:
				})
				.RunConsoleAsync();
		}
	}
}
