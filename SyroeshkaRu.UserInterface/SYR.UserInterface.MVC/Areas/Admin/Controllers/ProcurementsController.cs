using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	public class ProcurementsController : Controller
	{
		public async Task<IActionResult> Index() => await Task.Run(View);
	}
}