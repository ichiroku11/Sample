using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBindingWebApp.Models {
	public enum MonsterCategory {
		None = 0,

		[Display(Name = "スライム系")]
		Slime,

		[Display(Name = "けもの系")]
		Animal,

		[Display(Name = "鳥系")]
		Fly,
	}

	public static class MonsterCategoryExtensions {
		// todo:
		/*
		private static readonly object test = typeof(MonsterCategory)
			.GetFields()
			.SelectMany(field => field.GetV.GetCustomAttributes(false).OfType<DisplayAttribute>());
		*/
		private static readonly Dictionary<MonsterCategory, DisplayAttribute> _displayAttributes
			= Enum.GetValues(typeof(MonsterCategory))
				.OfType<MonsterCategory>()
				.ToDictionary(
					category => category,
					category => category
						.GetType()
						.GetField(category.ToString())
						.GetCustomAttributes(false)
						.OfType<DisplayAttribute>()
						.FirstOrDefault());

		public static string GetDisplayName(this MonsterCategory category)
			=> _displayAttributes[category]?.Name;

		public static SelectListItem ToSelectListItem(this MonsterCategory category) {
			var displayName = category == MonsterCategory.None
				? "選択してください"
				: category.GetDisplayName();

			return new SelectListItem {
				Text = displayName,
				Value = category.ToString().ToLower(),
			};
		}
	}
}
