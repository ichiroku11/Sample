using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SampleTest {
	public static class EnumHelper {
		public static Dictionary<TEnum, DisplayAttribute> GetDisplayAttributes<TEnum>()
			where TEnum : Enum {
			return typeof(TEnum)
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.ToDictionary(
					field => (TEnum)field.GetValue(null),
					field => field.GetCustomAttributes<DisplayAttribute>().FirstOrDefault());
		}
	}
}
