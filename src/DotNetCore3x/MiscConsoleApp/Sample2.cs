using System;
using System.Collections.Generic;
using System.Text;

namespace MiscConsoleApp {
	public class Sample2 : SampleBase, ISample {
		/// <summary>
		/// <see cref="Sample2.Property"/>のsummary
		/// </summary>
		public int Property { get; set; }
		/// <summary>
		/// <see cref="Sample2.Method"/>のsummary
		/// </summary>
		/// <returns></returns>
		public override int Method() => 2;
	}
}
