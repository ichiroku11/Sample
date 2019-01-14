using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AbstractModelBindingWebApp.Models {
	// GeometryModelのモデルバインダーを提供する
	public class GeometryModelBinderProvider : IModelBinderProvider {
		// モデルバインダーを取得
		public IModelBinder GetBinder(ModelBinderProviderContext context) {
			// 役割的にはモデルバインダープロバイダーで、
			// モデルの具象クラスを特定する方がスマートかと思ったけど、
			// ここではPOSTされたデータ（ValueProvider）にアクセスできない様子

			// バインドする型がGeometryModelなら、GeometryModelBinderを返すだけとする
			if (context.Metadata.ModelType != typeof(GeometryModel)) {
				return null;
			}

			return new GeometryModelBinder();
		}
	}
}
