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

			// inheridocを使わなくてもIntelliSenseが有効
			var sample1 = new Sample1();
			sample1.Method();
			sample1.Property = 1;

			CallMethod(sample1);
			GetProperty(sample1);

			// summaryは上書きできる
			var sample2 = new Sample2();
			sample2.Method();
			sample2.Property = 2;
		}
	}
}
