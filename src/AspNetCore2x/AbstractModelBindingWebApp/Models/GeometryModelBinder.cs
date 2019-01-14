using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace AbstractModelBindingWebApp.Models {
	// GeometryModelのモデルバインダー
	public class GeometryModelBinder : IModelBinder {
		// バインドするモデルの具象クラスの型を取得
		private Type GetModelType(IValueProvider valueProvider) {
			// POSTされたデータの中から"GeometryType"の値を取得
			var valueProviderResult = valueProvider.GetValue(nameof(GeometryModel.GeometryType));

			// 文字列をenumに変換
			if (!Enum.TryParse<GeometryType>(valueProviderResult.FirstValue, true, out var geometryType)) {
				throw new InvalidOperationException();
			}

			// enumからモデルの型を取得
			return geometryType.GetModelType();
		}

		// ModelBinderFactoryContextを取得
		private ModelBinderFactoryContext GetBinderFactoryContext(ModelMetadata metadata) {
			// AspNetCoreのコードを参考にしたが深く理解していない
			return new ModelBinderFactoryContext {
				Metadata = metadata,
				BindingInfo = new BindingInfo {
					BinderModelName = metadata.BinderModelName,
					BinderType = metadata.BinderType,
					BindingSource = metadata.BindingSource,
					PropertyFilterProvider = metadata.PropertyFilterProvider,
				},
				CacheToken = metadata,
			};
		}

		public Task BindModelAsync(ModelBindingContext bindingContext) {
			var services = bindingContext.HttpContext.RequestServices;

			// 実際にバインドするモデルの具象クラスの型を取得
			var type = GetModelType(bindingContext.ValueProvider);

			// モデルの型からModelMetadataを取得
			var metadataProvider = services.GetRequiredService<IModelMetadataProvider>();
			var metadata = metadataProvider.GetMetadataForType(type);

			// ModelMetadataからモデルバインダーを取得
			var binderFactory = services.GetRequiredService<IModelBinderFactory>();
			var binder = binderFactory.CreateBinder(GetBinderFactoryContext(metadata));

			// ModelMetadataを差し替えて（いいのか？という不安が）
			bindingContext.ModelMetadata = metadata;

			// バインド実行
			return binder.BindModelAsync(bindingContext);
		}
	}
}
