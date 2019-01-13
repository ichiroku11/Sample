using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractModelBindingWebApp.Models {
	// ジオメトリタイプ
	public enum GeometryType {
		Unknown = 0,
		// 線分
		Line = 1,
		// 円
		Circle = 2,
	}


	public static class GeometryTypeExtensions {
		// GeometryTypeからモデルの型を取得
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
