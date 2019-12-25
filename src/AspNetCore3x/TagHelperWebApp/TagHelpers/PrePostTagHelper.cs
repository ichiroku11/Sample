using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperWebApp.TagHelpers {
	public class PrePostTagHelper : TagHelper {
		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			output.TagName = "div";

			output.PreElement.AppendHtml($"<span>{nameof(output.PreElement)}</span>");
			output.PreContent.AppendHtml($"<span>{nameof(output.PreContent)}</span>");
			output.PostContent.AppendHtml($"<span>{nameof(output.PostContent)}</span>");
			output.PostElement.AppendHtml($"<span>{nameof(output.PostElement)}</span>");

			return Task.CompletedTask;
		}
	}
}
