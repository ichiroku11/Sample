using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CancelConsoleApp {
	class Program {
		private static readonly string _connectionString
			= new SqlConnectionStringBuilder {
				DataSource = ".",
				InitialCatalog = "Test",
				IntegratedSecurity = true,
			}.ToString();

		static async Task Main(string[] args) {
			var session = new EventSession(_connectionString);

			try {
				session
					.DropIfExists()
					.Create()
					.Start();

				// キャンセルしない
				var result = await new SampleQuery(_connectionString).RunAsync();
				Console.WriteLine(result);

				// todo:
				foreach (var @event in session.GetEvents()) {
					Console.WriteLine(@event.SqlText);
					Console.WriteLine(@event.Result);
				}
			} finally {
				session.DropIfExists();
			}

			try {
				session
					.DropIfExists()
					.Create()
					.Start();

				// キャンセルする
				var timeout = TimeSpan.FromSeconds(2);  // 2秒後
				var tokenSource = new CancellationTokenSource(timeout);

				var result = await new SampleQuery(_connectionString).RunAsync(tokenSource.Token);
				Console.WriteLine(result);

				// todo:
				foreach (var @event in session.GetEvents()) {
					Console.WriteLine(@event.SqlText);
					Console.WriteLine(@event.Result);
				}
			} finally {
				session.DropIfExists();
			}

			// todo: use parameter
		}
	}
}
