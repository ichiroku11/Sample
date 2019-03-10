using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace CancelConsoleApp {
	class Program {
		private static readonly string _connectionString
			= new SqlConnectionStringBuilder {
				DataSource = ".",
				InitialCatalog = "Test",
				IntegratedSecurity = true,
			}.ToString();

		static void Main(string[] args) {
			Console.WriteLine("Hello World!");

			using(var connection = new SqlConnection(_connectionString)) {
				var sql = "select 100;";
				var result = connection.Query<int>(sql).First();
				Console.WriteLine(result);
			}
		}
	}
}
