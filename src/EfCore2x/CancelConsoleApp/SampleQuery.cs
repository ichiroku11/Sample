using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancelConsoleApp {
	// サンプルクエリ
	public class SampleQuery {
		// パラメータを使わないSqlCommandを生成
		private static SqlCommand CreateCommandNoParameter(SqlConnection connection) {
			// 5s待機して1を取得
			const string sql = @"
-- CancelTest
waitfor delay '00:00:05';
select 1;";

			return new SqlCommand(sql, connection);
		}

		// パラメータを使うSqlCommandを生成
		private static SqlCommand CreateCommandWithParameter(SqlConnection connection) {
			// 5s待機して2を取得
			const string sql = @"
-- CancelTest
waitfor delay '00:00:05';
select @p;";

			var command = new SqlCommand(sql, connection);
			command.Parameters.AddWithValue("@p", 2);
			return command;
		}


		private readonly string _connectionString;
		private readonly bool _useParameter;


		public SampleQuery(string connectionString, bool useParameter = false) {
			_connectionString = connectionString;
			_useParameter = useParameter;
		}


		private SqlCommand CreateCommand(SqlConnection connection) {
			return _useParameter
				? CreateCommandWithParameter(connection)
				: CreateCommandNoParameter(connection);
		}

		// クエリを実行する
		public async Task<int> RunAsync(CancellationToken token) {
			using (var connection = new SqlConnection(_connectionString)) {
				// コネクションを開く
				await connection.OpenAsync(token);

				using (var command = CreateCommand(connection)) {
					try {
						// SQLを実行
						Console.WriteLine(nameof(SqlCommand.ExecuteReaderAsync));
						using (var reader = await command.ExecuteReaderAsync(token)) {
							// 結果を読み込む
							Console.WriteLine(nameof(SqlDataReader.ReadAsync));
							await reader.ReadAsync(token);

							// とりあえず1行目だけを読み込む
							Console.WriteLine(nameof(SqlDataReader.GetFieldValueAsync));
							return await reader.GetFieldValueAsync<int>(0, token);
						}
					} catch (Exception exception) {
						Console.WriteLine(exception);
					}
				}
			}

			return -1;
		}

		// クエリを実行
		public Task<int> RunAsync() => RunAsync(CancellationToken.None);
	}
}
