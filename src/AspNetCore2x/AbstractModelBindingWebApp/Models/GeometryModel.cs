using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbstractModelBindingWebApp.Models {
	// ジオメトリ
	// 今回のサンプルではインターフェイスでもいいかも
	public abstract class GeometryModel {
		public abstract GeometryType GeometryType { get; }
	}
}
