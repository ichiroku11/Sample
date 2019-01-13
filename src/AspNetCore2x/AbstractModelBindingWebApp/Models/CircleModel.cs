using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractModelBindingWebApp.Models {
	// 円
	public class CircleModel : GeometryModel {
		public override GeometryType GeometryType => GeometryType.Circle;

		// 中心座標と半径
		public int X { get; set; }
		public int Y { get; set; }
		public int R { get; set; }
	}
}
