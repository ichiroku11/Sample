using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace CancelConsoleApp {
	// イベントセッションを扱う
	public class EventSession {
		private readonly string _connectionString;


		public EventSession(string connectionString) {
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
		public void DropIfExists() {
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
alter event session test_xes
	on server
	state = start;";

				connection.Execute(sql);
			}
		}

		// イベントを取得
		public IEnumerable<Event> GetEvents() {
			using(var connection = new SqlConnection(_connectionString)) {
				const string sql = @"
with xe_e(data)
as(
	select
		cast(xe_st.target_data as xml)  -- データ（xml）
	from sys.dm_xe_sessions as xe_s
		inner join sys.dm_xe_session_targets as xe_st
			on xe_s.address = xe_st.event_session_address
	where xe_s.name = N'test_xes'
)
select
	node.value('@package', 'nvarchar(10)') as PackageName,
	node.value('@name', 'nvarchar(20)') as EventName,
	node.value('@timestamp', 'datetime2') as Timestamp,
	node.value('(data[@name=""duration""]/value)[1]', 'bigint') as Duration,
	node.value('(data[@name=""batch_text""]/value)[1]', 'nvarchar(max)') as BatchText,
	node.value('(data[@name=""statement""]/value)[1]', 'nvarchar(max)') as Statement,
	node.value('(data[@name=""result""]/value)[1]', 'int') as Result
from xe_e
	cross apply[data].nodes('/RingBufferTarget/event') as event (node);";

				return connection.Query<Event>(sql).ToList();
			}
		}
	}
}
