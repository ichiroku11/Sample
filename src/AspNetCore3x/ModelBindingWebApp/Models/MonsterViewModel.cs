using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBindingWebApp.Models {
	public class MonsterViewModel {
		public IEnumerable<SelectListItem> CategorySelectListItems { get; set; }
		public MonsterFormModel Form { get; set; }
	}
}
