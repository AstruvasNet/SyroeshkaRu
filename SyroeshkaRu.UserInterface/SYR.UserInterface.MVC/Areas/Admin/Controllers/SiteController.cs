using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("cp/site")]
    public class SiteController : Controller
    {
		[HttpGet("news")]
		public async Task<IActionResult> Index()
		{
			return await Task.Run(View);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Banners()
		{
			return await Task.Run(View);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Settings()
		{
			return await Task.Run(View);
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> Pages()
		{
			return await Task.Run(View);
		}
    }
}