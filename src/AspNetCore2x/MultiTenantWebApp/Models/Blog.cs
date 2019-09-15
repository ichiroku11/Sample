using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models {
	public class Blog {
		public int Id { get; set; }
		public int TenantId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
	}
}
