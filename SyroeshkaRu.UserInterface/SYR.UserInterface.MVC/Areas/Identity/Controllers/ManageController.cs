using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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