using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelBindingWebApp.Helpers;
using ModelBindingWebApp.Models;

namespace ModelBindingWebApp.Controllers {
	public class MonsterController : Controller {
		[LoadModelState]
		public IActionResult Index() {
			var viewModel = new MonsterViewModel {
				CategorySelectListItems = Enum.GetValues(typeof(MonsterCategory))
					.OfType<MonsterCategory>()
					.Select(category => category.ToSelectListItem()),
				Form = new MonsterFormModel {
					Id = 1,
					Category = MonsterCategory.Slime,
					Name = "スライム"
				},
			};

			return View(viewModel);
		}

		[HttpPost]
		[SaveModelState]
		public IActionResult Save([Bind(Prefix = nameof(MonsterViewModel.Form))]MonsterFormModel formModel) {
			if (!ModelState.IsValid) {
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
