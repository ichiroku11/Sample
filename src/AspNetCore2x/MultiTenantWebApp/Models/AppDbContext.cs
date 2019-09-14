using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models {
	public class AppDbContext : DbContext {
		private readonly ITenantIdProvider<int> _tenantIdProvider;

		public AppDbContext(ITenantIdProvider<int> tenantIdProvider) {
			_tenantIdProvider = tenantIdProvider;
		}

		// todo:
		protected int TenantId => _tenantIdProvider.GetTenantId();

		public DbSet<Blog> Blogs { get; set; }
	}
}
