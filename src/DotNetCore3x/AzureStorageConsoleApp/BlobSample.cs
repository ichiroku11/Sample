using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageConsoleApp {
	public class BlobSample {
		private readonly string _connectionString;
		private readonly ILogger _logger;

		public BlobSample(IConfiguration config, ILogger<BlobSample> logger) {
			_connectionString = config.GetConnectionString("Storage");
			_logger = logger;
		}

		public Task RunAsync() {
			_logger.LogInformation(nameof(RunAsync));

			// https://docs.microsoft.com/ja-jp/azure/storage/blobs/storage-quickstart-blobs-dotnet




			return Task.CompletedTask;
		}
	}
}
