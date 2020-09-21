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
			// 雑な実装（本当ならDisplayAttributeかな）
			return layout switch {
				OneLineLayout.SuperCentered => "01. 上下中央揃え: place-items: center",
				OneLineLayout.DeconstructedPancake => "02. パンケーキ解体レイアウト: flex: grow | shrink | baseWidth",
				OneLineLayout.SidebarSays => "03. サイドバー付きレイアウト: grid-template-columns: minmax(, ) …)",
				OneLineLayout.PancakeStack => "04. パンケーキ重ねレイアウト: grid-template-rows: auto 1fr auto",
				OneLineLayout.ClassicHolyGrailLayout => "05. 聖杯レイアウト: grid-template: auto 1fr auto / auto 1fr auto",
				OneLineLayout.TwelveSpanGrid => "06. 12カラムグリッド: grid-template-columns: repeat(12, 1fr)",
				OneLineLayout.RepeatAutoMinMax => "07. RAM(Repeat, Auto, MinMax): grid-template-columns(auto-fit, minmax(, 1fr))",
				OneLineLayout.LineUp => "08. 要素の整列: justify-content: space-between",
				OneLineLayout.ClampingMyStyle => "09. clamp(最小, 実サイズ, 最大)",
				OneLineLayout.RespectForAspect => "10. 画像などのアスペクト比を維持: aspect-ratio: 幅 / 高さ",
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
