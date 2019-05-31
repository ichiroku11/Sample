using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	public static class ApplicationLifetimeExtensions {
		// アプリケーションがスタートされるまで待機する
		public static async Task WaitForAppStartAsync(this IApplicationLifetime lifetime) {
			try {
				// アプリケーションがスタートしたらキャンセルされるキャンセルトークンのキャンセルを待機する
				await Task.Delay(Timeout.Infinite, lifetime.ApplicationStarted);
			} catch (TaskCanceledException) {
				// 何もしない
			}
		}
	}
}
