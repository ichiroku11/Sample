using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperWebApp.Models {
	public class ModalPartialViewModel {
		public IHtmlContentContainer BodyContent { get; set; }
	}
}
