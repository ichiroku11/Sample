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
		public void SemaphoreSlimを使ってみる() {
			// Arrange
			// Act
			// Assert
			var semaphore = new SemaphoreSlim(0);

			Assert.Equal(0, semaphore.CurrentCount);
		}

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
