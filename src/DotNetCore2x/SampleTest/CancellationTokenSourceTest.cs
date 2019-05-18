using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest {
	public class CancellationTokenSourceTest : IDisposable {
		private readonly CancellationTokenSource _source;

		public CancellationTokenSourceTest() {
			_source = new CancellationTokenSource();
		}

		public void Dispose() {
			_source?.Dispose();
		}

		// 参考
		// https://docs.microsoft.com/ja-jp/dotnet/api/system.threading.cancellationtokensource.cancel?view=netframework-4.8
		[Fact]
		public async Task Cancel_awaitするとTaskCanceledExceptionがスローされる() {
			// Arrange
			var token = _source.Token;

			// Act
			var task = Task.Delay(Timeout.Infinite, token);

			_source.Cancel();

			// Assert
			await Assert.ThrowsAsync<TaskCanceledException>(async () => {
				await task;
			});
		}
	}

}
