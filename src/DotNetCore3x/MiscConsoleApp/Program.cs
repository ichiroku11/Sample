using System;

namespace MiscConsoleApp {
	class Program {
		private static void CallMethod(SampleBase sample) {
			sample.Method();
		}

		private static int GetProperty(ISample sample) {
			return sample.Property;
		}

		static void Main(string[] args) {
			Console.WriteLine("Hello World!");

			var sample = new Sample1();
			sample.Method();
			sample.Property = 1;

			CallMethod(sample);
			GetProperty(sample);
		}
	}
}
