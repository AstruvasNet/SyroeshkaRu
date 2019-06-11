using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	public class ProcurementsController : Controller
	{
		public async Task<IActionResult> Index() => await Task.Run(View);
	}
}