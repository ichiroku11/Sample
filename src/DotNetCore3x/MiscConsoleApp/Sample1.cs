using System;
using System.Collections.Generic;
using System.Text;

namespace MiscConsoleApp {
	public class Sample1 : SampleBase, ISample {
		public int Property { get; set; }
		public override int Method() => 1;
	}
}
