using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models {
	public static class ClaimsPrincipalExtensions {
		public static int GetTenantId(this ClaimsPrincipal principal) {
			// クレームからテナントIDを取得
			var claim = principal.FindFirst(AppClaimTypes.TenantId);
			return int.Parse(claim.Value);
		}
	}
}
