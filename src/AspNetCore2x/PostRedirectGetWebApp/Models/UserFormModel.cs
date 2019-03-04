using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostRedirectGetWebApp.Models {
	public class UserFormModel {
		[Required]
		public string MailAddress { get; set; }
		public string DisplayName { get; set; }
	}
}
