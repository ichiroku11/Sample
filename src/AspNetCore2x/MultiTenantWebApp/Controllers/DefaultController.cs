using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantWebApp.Models;

namespace MultiTenantWebApp.Controllers {
	public class DefaultController : Controller {
		// 本当ならコントローラは直接DBコンテキストを持たないけど
		private readonly AppDbContext _dbContext;

		public DefaultController(AppDbContext dbContext) {
			_dbContext = dbContext;
		}

		public IActionResult Index(bool all = false) {
			var blogs = all
				// グローバルフィルタを無視する
				? _dbContext.Blogs.IgnoreQueryFilters()
				// グローバルフィルタ
				: _dbContext.Blogs;

			return View(blogs.ToList());
		}
	}
}
