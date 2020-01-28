using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageConsoleApp {
	public static class BlobContainerClientExtensions {
		public static Task UploadTextAsync(
			this BlobContainerClient containerClient, string blobName, string blobContent) {
			// todo:
			throw new NotImplementedException();
		}

		public static Task<string> DownloadTextAsync(
			this BlobContainerClient containerClient, string blobName) {
			// todo:
			throw new NotImplementedException();
		}
	}
}
