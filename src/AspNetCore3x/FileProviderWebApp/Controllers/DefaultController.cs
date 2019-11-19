using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace FileProviderWebApp.Controllers {
	public class DefaultController : Controller {
		// wwwrootフォルダのファイルプロバイダ
		private readonly IFileProvider _webRootFileProvider;
		// プロジェクト直下のフォルダのファイルプロバイダ
		private readonly IFileProvider _contentRootFileProvider;

		public DefaultController(IWebHostEnvironment env) {
			_webRootFileProvider = env.WebRootFileProvider;
			_contentRootFileProvider = env.ContentRootFileProvider;
		}

		public async Task<IActionResult> Index() {
			// ルートフォルダ（=スコープ）を指定してファイルプロバイダを生成
			var fileProvider = new PhysicalFileProvider(@"c:\temp");

			// IFileInfoを取得して
			var foundFile = fileProvider.GetFileInfo("test.txt");

			// ファイルの存在チェックができたり
			Debug.WriteLine(foundFile.Exists);
			// True

			// ファイルの内容を読み込んだり
			if (foundFile.Exists) {
				using var stream = foundFile.CreateReadStream();
				using var reader = new StreamReader(stream);
				var text = await reader.ReadToEndAsync();
			}

			// スコープ外のファイルには存在していてもアクセスできない
			var notFoundFile = fileProvider.GetFileInfo("c:\test.txt");
			Debug.WriteLine(notFoundFile.Exists);
			// False

			// 戻り値はNotFoundFileInfoというクラス
			Debug.WriteLine(notFoundFile.GetType());
			// Microsoft.Extensions.FileProviders.NotFoundFileInfo

			return new EmptyResult();
		}
	}
}
