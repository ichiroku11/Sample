using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace ValidatableObjectWebApp.Models {
	/// <summary>
	/// ユーザフォームモデル
	/// </summary>
	public class UserFormModel : IValidatableObject {
		[Display(Name = "ユーザ名")]
		public string UserName { get; set; }


		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
			// ValidationContextはIServiceProviderを実装している
			// ModelMetadataProviderを取得
			var metadataProvider = validationContext.GetRequiredService<IModelMetadataProvider>();

			// IValidatableObjectを使うまでもなく、
			// DataAnnotationsで済んでしまうが
			if (string.IsNullOrEmpty(UserName)) {
				// プロパティのModelMetadataを取得
				var metadata = metadataProvider.GetMetadataForProperty(GetType(), nameof(UserName));
				yield return new ValidationResult($"{metadata.DisplayName}を入力してください", new[] { nameof(UserName) });
			}
		}
	}
}
