using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageConsoleApp {
	public static class BlobContainerClientExtensions {
		public static async Task UploadTextAsync(
			this BlobContainerClient containerClient, string blobName, string blobContent) {
			using var stream = new MemoryStream(Encoding.UTF8.GetBytes(blobContent));

			var blobClient = containerClient.GetBlobClient(blobName);

			var exists = await blobClient.ExistsAsync();
			if (exists) {
				return;
			}

			await blobClient.UploadAsync(stream);
		}

		public static async Task<string> DownloadTextAsync(
			this BlobContainerClient containerClient, string blobName) {
			var blobClient = containerClient.GetBlobClient(blobName);
			var response = await blobClient.DownloadAsync();

			using var reader = new StreamReader(response.Value.Content, Encoding.UTF8);
			return await reader.ReadToEndAsync();
		}
	}
}
