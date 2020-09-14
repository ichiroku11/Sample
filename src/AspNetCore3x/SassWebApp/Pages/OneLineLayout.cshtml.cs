using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SassWebApp.Pages {
	public enum OneLineLayout {
		Index = 0,
		SuperCentered,
	}

	public class OneLineLayoutModel : PageModel {
		[BindProperty(SupportsGet = true)]
		public OneLineLayout Layout { get; set; } = OneLineLayout.Index;

		public string Subtitle => Layout switch {
			OneLineLayout.Index => nameof(Index),
			OneLineLayout.SuperCentered => "Super Centered: place-items: center",
			_ => throw new InvalidOperationException(),
		};
	}
}
