using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SassWebApp.Pages {
	public class GridLayoutModel : PageModel {
		[BindProperty(SupportsGet = true)]
		public int Pattern { get; set; } = 1;

		public void OnGet() {
		}
	}
}
