using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SYR.UserInterface.MVC.Areas.Identity.Controllers
{
	[Area("identity")]
	public class ManageController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return await Task.Run(View);
		}
	}
}