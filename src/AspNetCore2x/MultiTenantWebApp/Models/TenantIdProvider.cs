using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models {
	public class TenantIdProvider : ITenantIdProvider<int> {
		public int GetTenantId() {
			// todo:
			throw new NotImplementedException();
		}
	}
}
