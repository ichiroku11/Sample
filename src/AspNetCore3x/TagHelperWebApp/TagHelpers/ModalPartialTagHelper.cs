using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagHelperWebApp.Models;

namespace TagHelperWebApp.TagHelpers {
	public class ModalPartialTagHelper : PartialTagHelper {
		public ModalPartialTagHelper(ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope)
			: base(viewEngine, viewBufferScope) {
		}

		public string Id { get; set; }
		public string Title { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			Name = "_Modal";

			var bodyContent = await output.GetChildContentAsync();
			Model = new ModalPartialViewModel {
				Id = Id,
				Title = Title,
				BodyContent = bodyContent,
			};

			await base.ProcessAsync(context, output);
		}
	}
}
