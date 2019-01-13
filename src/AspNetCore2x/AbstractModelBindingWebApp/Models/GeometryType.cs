using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractModelBindingWebApp.Models {
	public enum GeometryType {
		Unknown = 0,
		Line = 1,
		Circle = 2,
	}

	public static class GeometryTypeExtensions {
		public static Type GetModelType(this GeometryType geometryType) {
			switch (geometryType) {
			case GeometryType.Line:
				return typeof(LineModel);
			case GeometryType.Circle:
				return typeof(CircleModel);
			default:
				throw new NotImplementedException();
			}
		}
	}
}
