using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Filters;
using System.Threading.Tasks;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	[
		Area("Admin"),
		Route("cp/site"),
		Sequrity
	]
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