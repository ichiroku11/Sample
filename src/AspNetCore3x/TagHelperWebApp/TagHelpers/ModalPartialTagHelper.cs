using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperWebApp.TagHelpers {
	public class ModalPartialTagHelper : PartialTagHelper {
		public ModalPartialTagHelper(ICompositeViewEngine viewEngine, IViewBufferScope viewBufferScope)
			: base(viewEngine, viewBufferScope) {
		}

		public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			Name = "_Modal";
			return base.ProcessAsync(context, output);
		}
	}
}
