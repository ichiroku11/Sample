using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerConsumerConsoleApp {
	public static class HexHelper {
		// 16進数文字列を取得
		public static string ToString(IEnumerable<byte> bytes) {
			var builder = new StringBuilder();

			foreach (var @byte in bytes) {
				builder.Append($"{@byte:x2}");
			}

			return builder.ToString();
		}
	}
}
