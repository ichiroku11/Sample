using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ControllerWebApp {
	public static class ICustomAttributeProviderExtensions {
		public static IEnumerable<TAttribute> GetAttributes<TAttribute>(
			this ICustomAttributeProvider provider, bool inherit) {
			return provider
				.GetCustomAttributes(typeof(TAttribute), inherit)
				.Cast<TAttribute>();
		}

		public static TAttribute GetAttribute<TAttribute>(
			this ICustomAttributeProvider provider, bool inherit) {
			return provider
				.GetAttributes<TAttribute>(inherit)
				.FirstOrDefault();
		}
	}
}
