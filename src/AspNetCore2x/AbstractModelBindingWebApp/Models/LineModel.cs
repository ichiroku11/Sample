using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractModelBindingWebApp.Models {
	public class LineModel : GeometryModel {
		public override GeometryType GeometryType => GeometryType.Line;

		public int X1 { get; set; }
		public int Y1 { get; set; }
		public int X2 { get; set; }
		public int Y2 { get; set; }
	}
}
