using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiscWebApi.Controllers {
	// ディクショナリへのバインドを試す
	// https://docs.microsoft.com/ja-jp/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1

	[Route("api/[controller]")]
	[ApiController]
	public class DictionaryController : ControllerBase {
		// todo: 単純型のディクショナリ
		// todo: 複合型のディクショナリ
	}
}
