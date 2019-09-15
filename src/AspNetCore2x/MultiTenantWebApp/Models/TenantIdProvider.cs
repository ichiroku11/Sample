using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models {
	public class TenantIdProvider : ITenantIdProvider<int> {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private int? _tenantId;

		public TenantIdProvider(IHttpContextAccessor httpContextAccessor) {
			_httpContextAccessor = httpContextAccessor;
		}

		public int GetTenantId() {
			if (!_tenantId.HasValue) {
				// クレームからテナントIDを取得
				var claim = _httpContextAccessor.HttpContext.User.FindFirst(AppClaimTypes.TenantId);
				_tenantId = int.Parse(claim.Value);
			}

			return _tenantId.Value;
		}
	}
}
