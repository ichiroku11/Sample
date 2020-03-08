using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBindingWebApp.Models {
	public class MonsterFormModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public MonsterCategory Category { get; set; }
	}
}
