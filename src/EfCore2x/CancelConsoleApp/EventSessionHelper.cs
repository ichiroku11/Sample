using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace CancelConsoleApp {
	// イベントセッションを扱う
	public class EventSessionHelper {
		private readonly string _connectionString;


		public EventSessionHelper(string connectionString) {
			_connectionString = connectionString;
		}


		// セッションを作成
		public void Create() {
			using (var connection = new SqlConnection(_connectionString)) {
				const string sql = @";
create event session test_xes
	on Server
	add event sqlserver.sql_batch_completed(
		where batch_text like N'-- CancelTest%'
	),
	add event sqlserver.rpc_completed(
		where statement like N'-- CancelTest%'
	)
	add target package0.ring_buffer;";

				connection.Execute(sql);
			}
		}

		// セッションを削除
		public void Drop() {
			using (var connection = new SqlConnection(_connectionString)) {
				const string sql = @";
if exists(
	select *
	from sys.dm_xe_sessions
	where name = N'test_xes')
	drop event session test_xes
		on Server;";

				connection.Execute(sql);
			}
		}

		// セッションを開始
		public void Start() {
			using (var connection = new SqlConnection(_connectionString)) {
				const string sql = @";
--セッション（キャプチャ）を開始
alter event session test_xes
	on server
	state = start;";

				connection.Execute(sql);
			}
		}
	}
}
