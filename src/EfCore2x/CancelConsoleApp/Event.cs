using System;
using System.Collections.Generic;
using System.Text;

namespace CancelConsoleApp {
	// イベント
	public class Event {
		// パッケージ名
		public string PackageName { get; set; }

		// イベント名
		public string EventName { get; set; }

		// タイムスタンプ
		public DateTime Timestamp { get; set; }

		// 実行時間
		public long Duration { get; set; }

		// sql_batch_completedイベントのSQL文字列
		public string BatchText { get; set; }

		// rpc_completedイベントのSQL文字列
		public string Statement { get; set; }

		// SQLを取得
		public string SqlText => string.IsNullOrEmpty(BatchText) ? Statement : BatchText;

		// 実行結果
		// 0: OK
		// 1: Error
		// 2: Abort
		public int Result { get; set; }
	}
}
