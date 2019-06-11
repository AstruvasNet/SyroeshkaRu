using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Mapping;
using SYR.Core.BusinessLogic.Service;
using SYR.UserInterface.MVC.Models;

namespace SYR.UserInterface.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISyroeshkaRu _db;

		public HomeController(ISyroeshkaRu db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			return await Task.Run(View);
		}

		public async Task<IActionResult> Privacy()
		{
			return await Task.Run(View);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
