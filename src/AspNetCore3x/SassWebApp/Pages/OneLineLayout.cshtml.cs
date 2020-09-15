using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SassWebApp.Pages {
	public enum OneLineLayout {
		SuperCentered = 1,
	}

	public static class OneLineLayoutExtensions {
		public static string GetDisplayName(this OneLineLayout layout) {
			// 雑・・・
			return layout switch {
				OneLineLayout.SuperCentered => "Super Centered: place-items: center",
				_ => throw new InvalidOperationException(),
			};
		}
	}

	public class OneLineLayoutModel : PageModel {
		[BindProperty(SupportsGet = true)]
		public OneLineLayout Layout { get; set; } = OneLineLayout.SuperCentered;

		public void OnGet() {
		}
	}
}
