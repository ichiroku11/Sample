using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models {
	public class TenantIdProvider : ITenantIdProvider {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private int? _tenantId;

		public TenantIdProvider(IHttpContextAccessor httpContextAccessor) {
			_httpContextAccessor = httpContextAccessor;
		}

		public int GetTenantId() {
			if (!_tenantId.HasValue) {
				// クレームからテナントIDを取得
				_tenantId = _httpContextAccessor.HttpContext.User.GetTenantId();
			}

			return _tenantId.Value;
		}
	}
}
