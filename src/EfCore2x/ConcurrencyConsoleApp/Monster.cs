using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConcurrencyConsoleApp {
	public class Monster {
		public int Id { get; set; }
		public string Name { get; set; }
		public int Hp { get; set; }
		[Timestamp]
		public byte[] Ver { get; set; }
	}
}
