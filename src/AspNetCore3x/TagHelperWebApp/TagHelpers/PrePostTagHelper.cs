using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperWebApp.TagHelpers {
	public class PrePostTagHelper : TagHelper {
		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			// div要素に変換
			output.TagName = "div";

			// div要素にclass属性を設定
			output.Attributes.Add("class", "content");

			// コンテンツ"内"の前後にhtmlを挿入
			output.PreContent.SetHtmlContent($"<span class=\"pre-content\">{nameof(output.PreContent)}</span>");
			output.PostContent.SetHtmlContent($"<span class=\"post-content\">{nameof(output.PostContent)}</span>");

			// コンテンツ"外"の前後にhtmlを挿入
			output.PreElement.SetHtmlContent($"<div class=\"pre-element\">{nameof(output.PreElement)}</div>");
			output.PostElement.SetHtmlContent($"<div class=\"post-element\">{nameof(output.PostElement)}</div>");

			return Task.CompletedTask;
		}
	}
}
