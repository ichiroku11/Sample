﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostRedirectGetWebApp.Models {
	public class UserFormModel {
		[Display(Name = "メールアドレス")]
		[Required]
		public string MailAddress { get; set; }

		[Display(Name = "表示名")]
		public string DisplayName { get; set; }
	}
}
