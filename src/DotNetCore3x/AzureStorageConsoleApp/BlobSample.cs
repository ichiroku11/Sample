using Azure.Storage.Blobs;
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

		public async Task RunAsync() {
			_logger.LogInformation(nameof(RunAsync));

			// 参考
			// https://docs.microsoft.com/ja-jp/azure/storage/blobs/storage-quickstart-blobs-dotnet

			var serviceClient = new BlobServiceClient(_connectionString);

			var containerClient = serviceClient.GetBlobContainerClient("sample");
			// todo:
			var container = await containerClient.CreateIfNotExistsAsync();

			// BLOB一覧を取得
			await foreach (var item in containerClient.GetBlobsAsync()) {
				_logger.LogInformation(item.Name);
			}
		}
	}
}
