using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerConsoleApp {
	// Producer、Consumerの基本クラス
	public abstract class WorkerService : BackgroundService {
		private readonly IApplicationLifetime _lifetime;

		public WorkerService(IApplicationLifetime lifetime) {
			_lifetime = lifetime;
		}

		// コア処理を実行する
		protected abstract Task ExecuteCoreAsync(CancellationToken stoppingToken);

		protected override Task ExecuteAsync(CancellationToken stoppingToken) {
			return Task.Run(
				async () => {
					// アプリケーションが開始されるまで待つ
					await _lifetime.WaitForAppStartAsync();

					// メイン処理開始
					await ExecuteCoreAsync(stoppingToken);
				},
				stoppingToken);
		}
	}
}
