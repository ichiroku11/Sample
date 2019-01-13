using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractModelBindingWebApp.Models {
	public class CircleModel : GeometryModel {
		public override GeometryType GeometryType => GeometryType.Circle;

		public int X { get; set; }
		public int Y { get; set; }
		public int R { get; set; }
	}
}
