using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TagHelperWebApp.Models;

namespace TagHelperWebApp.Controllerss {
	public class DefaultController : Controller {
		private readonly ILogger _logger;

		public DefaultController(ILogger<DefaultController> logger) {
			_logger = logger;
		}

		public IActionResult Index() {
			return View();
		}

		public IActionResult Form() {
			return View();
		}

		[HttpPost]
		public IActionResult Form(FormCommand command) {
			foreach (var modelState in ModelState) {
				var builder = new StringBuilder();
				builder.Append("ModelState:")
					.Append($" {nameof(modelState.Key)}={modelState.Key}")
					.Append($", {nameof(modelState.Value.RawValue)}.Type={modelState.Value.RawValue.GetType()}")
					.Append($", {nameof(modelState.Value.RawValue)}={modelState.Value.RawValue}");
				_logger.LogInformation(builder.ToString());
			}

			return RedirectToAction(nameof(Form));
		}
	}
}
