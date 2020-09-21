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
		SidebarSays,
		PancakeStack,
		ClassicHolyGrailLayout,
		TwelveSpanGrid,
		RepeatAutoMinMax,
		LineUp,
		ClampingMyStyle,
		RespectForAspect,
	}

	public static class OneLineLayoutExtensions {
		public static string GetDisplayName(this OneLineLayout layout) {
			// 雑・・・
			return layout switch {
				OneLineLayout.SuperCentered => "01. Super Centered: place-items: center",
				OneLineLayout.DeconstructedPancake => "02. The Deconstructed Pancake: flex: <grow> <shrink> <baseWidth>",
				OneLineLayout.SidebarSays => "03. Sidebar Says: grid-template-columns: minmax(<min>, <max>) ...)",
				OneLineLayout.PancakeStack => "04. Pancake Stack: grid-template-rows: auto 1fr auto",
				OneLineLayout.ClassicHolyGrailLayout => "05. Classic Holy Grail Layout: grid-template: auto 1fr auto / auto 1fr auto",
				OneLineLayout.TwelveSpanGrid => "06. 12-Span Grid: grid-template-columns: repeat(12, 1fr)",
				OneLineLayout.RepeatAutoMinMax => "07. RAM (Repeat, Auto, MinMax): grid-template-columns(auto-fit, minmax(<base>, 1fr))",
				OneLineLayout.LineUp => "08. Line Up: justify-content: space-between",
				OneLineLayout.ClampingMyStyle => "09. Clamping My Style: clamp(<min>, <actual>, <max>)",
				OneLineLayout.RespectForAspect => "10. Respect for Aspect: aspect-ratio: <width> / <height>",
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
