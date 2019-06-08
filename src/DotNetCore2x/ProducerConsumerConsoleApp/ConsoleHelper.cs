using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerConsumerConsoleApp {
	public class ConsoleHelper : IConsoleHelper {
		private static readonly object _lock = new object();

		public void WriteLine(string message, ConsoleColor color) {
			// キューにためて処理した方がいい気がする
			lock (_lock) {
				Console.ForegroundColor = color;
				Console.WriteLine(message);
				Console.ResetColor();
			}
		}
	}
}
