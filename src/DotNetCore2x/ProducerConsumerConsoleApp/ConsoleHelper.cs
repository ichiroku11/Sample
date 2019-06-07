using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerConsumerConsoleApp {
	public static class ConsoleHelper {

		private static readonly object _lock = new object();

		public static void WriteLine(string message, ConsoleColor color) {
			lock (_lock) {
				Console.ForegroundColor = color;
				Console.WriteLine(message);
				Console.ResetColor();
			}
		}
	}
}
