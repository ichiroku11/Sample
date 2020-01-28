using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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

		private async Task UpoladBlobAsync(
			BlobContainerClient containerClient, string blobName, string blobContent) {
			using var stream = new MemoryStream(Encoding.UTF8.GetBytes(blobContent));

			var blobClient = containerClient.GetBlobClient(blobName);

			var exists = await blobClient.ExistsAsync();
			if (exists) {
				return;
			}

			var response = await blobClient.UploadAsync(stream);
		}

		private async Task<string> DownloadBlobAsync(
			BlobContainerClient containerClient, string blobName) {

			var blobClient = containerClient.GetBlobClient(blobName);
			var response = await blobClient.DownloadAsync();

			using var reader = new StreamReader(response.Value.Content);
			return await reader.ReadToEndAsync();
		}

		public async Task RunAsync() {
			_logger.LogInformation(nameof(RunAsync));
			// 参考
			// https://docs.microsoft.com/ja-jp/azure/storage/blobs/storage-quickstart-blobs-dotnet

			var serviceClient = new BlobServiceClient(_connectionString);

			var containerClient = serviceClient.GetBlobContainerClient("sample");

			// todo:
			var container = await containerClient.CreateIfNotExistsAsync();

			// Blobをアップロード
			await UpoladBlobAsync(containerClient, "a.txt", "Aaa");
			await UpoladBlobAsync(containerClient, "b.txt", "Bbb");

			// Blob一覧
			await foreach (var item in containerClient.GetBlobsAsync()) {
				_logger.LogInformation(item.Name);
			}

			// Blobをダウンロード
			_logger.LogInformation(await DownloadBlobAsync(containerClient, "a.txt"));
			_logger.LogInformation(await DownloadBlobAsync(containerClient, "b.txt"));
		}
	}
}
