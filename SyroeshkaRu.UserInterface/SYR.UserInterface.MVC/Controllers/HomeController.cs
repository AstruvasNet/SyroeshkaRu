using Microsoft.AspNetCore.Mvc;
using SYR.UserInterface.MVC.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SYR.UserInterface.MVC.Controllers
{
	public class HomeController : Controller
	{
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