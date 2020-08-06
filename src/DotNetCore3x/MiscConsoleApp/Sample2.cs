using System;
using System.Collections.Generic;
using System.Text;

namespace MiscConsoleApp {
	public class Sample2 : SampleBase, ISample {
		public int Property { get; set; }
		public override int Method() => 2;
	}
}
