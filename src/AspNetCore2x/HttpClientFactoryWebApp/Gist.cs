using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientFactoryWebApp {
	// 最小限のプロパティのみ
	public class Gist {
		public string Id { get; set; }
		public Dictionary<string, GistFile> Files { get; set; }
	}
}
