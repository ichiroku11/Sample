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

		private static async Task<int> Run(CancellationToken token) {
			using (var connection = new SqlConnection(_connectionString)) {
				await connection.OpenAsync(token);

				var sql = @"
waitfor delay '00:00:05';
select 100;";
				using (var command = new SqlCommand(sql, connection))
				using (var reader = await command.ExecuteReaderAsync(token)) {
					while (await reader.ReadAsync(token)) {
						var value = await reader.GetFieldValueAsync<int>(0, token);

						return value;
					}
				}
			}

			return -1;
		}


		static async Task Main(string[] args) {
			Console.WriteLine("Hello World!");

			var result = await Run(CancellationToken.None);
			Console.WriteLine(result);

			/*
			var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));
			try {
				await Run(tokenSource.Token);
			} catch (Exception) {
				throw;
			}
			*/
		}
	}
}
