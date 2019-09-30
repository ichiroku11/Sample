using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SampleTest {
	public class SemaphoreSlimTest {
		private readonly ITestOutputHelper _output;

		public SemaphoreSlimTest(ITestOutputHelper output) {
			_output = output;
		}

		[Fact]
		public void 使ってみる() {
			// Arrange
			// Act
			// Assert
			using (var semaphore = new SemaphoreSlim(0)) {
				Assert.Equal(0, semaphore.CurrentCount);
			}
		}

		[Fact]
		public void Release_セマフォに残っている数が増える() {
			// Arrange
			// Act
			// Assert
			using (var semaphore = new SemaphoreSlim(0)) {
				Assert.Equal(0, semaphore.CurrentCount);

				// リリースするごとにセマフォに残っている数は増える
				semaphore.Release();
				Assert.Equal(1, semaphore.CurrentCount);

				semaphore.Release();
				Assert.Equal(2, semaphore.CurrentCount);
			}
		}

		[Fact]
		public void Release_最大値までセマフォに残っている数が増える() {
			// Arrange
			// Act
			// Assert
			// 初期値と最大値を指定
			using (var semaphore = new SemaphoreSlim(0, 1)) {
				Assert.Equal(0, semaphore.CurrentCount);

				// リリースするとセマフォに残っている数が増えるが
				semaphore.Release();
				Assert.Equal(1, semaphore.CurrentCount);

				// 最大値を超えてリリースすると例外がスロー
				Assert.Throws<SemaphoreFullException>(() => {
					semaphore.Release();
				});
			}
		}

		// todo: これダメだ
		/*
		[Fact]
		public async Task 使ってみる2() {
			// Arrange
			// Act
			// Assert
			using (var semaphore = new SemaphoreSlim(1)) {
				var step = 0;

				var task = Task.Run(async () => {
					await semaphore.WaitAsync();

					Interlocked.Increment(ref step);
				});

				Assert.Equal(0, step);

				semaphore.Release();

				Assert.Equal(1, step);

				await task;
			}
		}
		*/

		// todo: 上手く書けない
		/*
		[Fact]
		public void SemaphoreSlimを使ってみる2() {
			// Arrange
			// Act
			// Assert
			var semaphore = new SemaphoreSlim(0);

			var task1 = Task.Run(() => {
				_output.WriteLine("Before wait");
				semaphore.Wait();
				_output.WriteLine("After wait");
				return 1;
			});
			var task2 = Task.Run(() => {
				Thread.Sleep(500);
				_output.WriteLine("Before release");
				semaphore.Release();
				_output.WriteLine("After release");
			});

			Assert.Equal(1, task1.Result);
		}
		*/
	}
}
