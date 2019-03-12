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
				Console.WriteLine(nameof(SqlConnection.OpenAsync));
				await connection.OpenAsync(token);

				// 5秒待って値を取得する
				var sql = @"
-- CancelTest
waitfor delay '00:00:05';
select 100;";

				using (var command = new SqlCommand(sql, connection)) {
					try {
						Console.WriteLine(nameof(SqlCommand.ExecuteReaderAsync));
						using (var reader = await command.ExecuteReaderAsync(token)) {

							Console.WriteLine(nameof(SqlDataReader.ReadAsync));
							await reader.ReadAsync(token);

							Console.WriteLine(nameof(SqlDataReader.GetFieldValueAsync));
							return await reader.GetFieldValueAsync<int>(0, token);
						}
					} catch (Exception exception) {
						// todo:
						Console.WriteLine("catch:");
						Console.WriteLine(exception);
					}
				}
			}

			return -1;
		}

		static async Task Main(string[] args) {
			{
				// キャンセルしない
				await QueryAsync(CancellationToken.None);
			}

			{
				// キャンセルする
				// 2秒後
				var timeout = TimeSpan.FromSeconds(2);
				var tokenSource = new CancellationTokenSource(timeout);

				await QueryAsync(tokenSource.Token);
			}
		}
	}
}
