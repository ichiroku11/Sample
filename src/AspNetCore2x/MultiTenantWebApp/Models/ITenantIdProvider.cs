using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models {
	public interface ITenantIdProvider<TValue> {
		TValue GetTenantId();
	}
}
