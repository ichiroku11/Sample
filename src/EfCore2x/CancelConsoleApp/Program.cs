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
				session.DropIfExists();
				session.Create();
				session.Start();

				{
					// キャンセルしない
					var result = await new SampleQuery(_connectionString).RunAsync(CancellationToken.None);
					Console.WriteLine(result);
				}
				/*
				{
					// キャンセルする

					// 2秒後
					var timeout = TimeSpan.FromSeconds(2);
					var tokenSource = new CancellationTokenSource(timeout);

					var result = await new SampleQuery(_connectionString).RunAsync(tokenSource.Token);
					Console.WriteLine(result);
				}
				*/
				// todo:
				foreach (var @event in session.GetEvents()) {
					Console.WriteLine(@event.SqlText);
				}
			} finally {
				// session.Drop();
			}
		}
	}
}
