using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerConsumerConsoleApp {
	public interface IConsoleHelper {
		void WriteLine(string message, ConsoleColor color);
	}
}
