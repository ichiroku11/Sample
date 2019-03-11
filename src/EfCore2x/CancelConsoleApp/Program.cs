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

		private static async Task<int> QueryAsync(CancellationToken token) {
			using (var connection = new SqlConnection(_connectionString)) {
				await connection.OpenAsync(token);

				var sql = @"
-- CancelTest
waitfor delay '00:00:05';
select 100;";

				using (var command = new SqlCommand(sql, connection)) {
					try {
						Console.WriteLine("ExecuteReaderAsync");
						using (var reader = await command.ExecuteReaderAsync(token)) {

							Console.WriteLine("ReadAsync");
							while (await reader.ReadAsync(token)) {
								var value = await reader.GetFieldValueAsync<int>(0, token);

								return value;
							}
						}
					} catch (Exception exception) {
						Console.WriteLine("catch:");
						Console.WriteLine(exception);
					}
				}
			}

			return -1;
		}

		static async Task Main(string[] args) {
			// キャンセルしない
			{
				await QueryAsync(CancellationToken.None);
			}

			// キャンセルする
			{
				var timeout = TimeSpan.FromSeconds(2);
				var tokenSource = new CancellationTokenSource(timeout);

				await QueryAsync(tokenSource.Token);
			}
		}
	}
}
