using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest {
	public class TaskTest {
		[Fact]
		public async Task FromCanceled_キャンセルされたCancellationTokenを渡す() {
			// Arrange
			// Act
			// キャンセルされたCancellationTokenを渡す必要がある
			var task = Task.FromCanceled(new CancellationToken(true));

			// Assert
			await Assert.ThrowsAsync<TaskCanceledException>(async () => await task);
		}

		[Fact]
		public void FromCanceled_キャンセルされていないCancellationTokenを渡すとArgumentOutOfRangeException() {
			// Arrange
			// Act
			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(() => {
				// キャンセルされていないCancellationTokenを渡すと例外
				Task.FromCanceled(new CancellationToken(false));
			});
		}

		[Fact]
		public void Wait_例外はAggregateExceptionに集約される() {
			// Arrange
			// Act
			var task = Task.FromCanceled(new CancellationToken(true));

			// Assert
			var exception = Assert.Throws<AggregateException>(() => task.Wait());
			Assert.Single(exception.InnerExceptions);
			Assert.IsType<TaskCanceledException>(exception.InnerException);
		}
	}
}
