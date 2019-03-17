using System;
using System.Collections.Generic;
using System.Text;

namespace CancelConsoleApp {
	public static class StringExtensions {
		public static string PrependTag(this string sql, string tag)
			=> sql.Insert(0, $@"-- {tag}{Environment.NewLine}");
	}
}
