using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SassWebApp.Pages {
	public enum OneLineLayout {
		SuperCentered = 1,
		DeconstructedPancake,
	}

	public static class OneLineLayoutExtensions {
		public static string GetDisplayName(this OneLineLayout layout) {
			// 雑・・・
			return layout switch {
				OneLineLayout.SuperCentered => "01. Super Centered: place-items: center",
				OneLineLayout.DeconstructedPancake => "02. The Deconstructed Pancake: flex: <grow> <shrink> <baseWidth>",
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
