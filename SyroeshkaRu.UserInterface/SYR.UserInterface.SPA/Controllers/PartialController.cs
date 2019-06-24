using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Interface;

namespace SYR.UserInterface.SPA.Controllers
{
	public class PartialController : Controller
	{
		private readonly ISyroeshkaRu _db;

		public PartialController(ISyroeshkaRu db)
		{
			_db = db;
		}

		[Route("[controller]/[action]/{id?}")]
		public async Task<IActionResult> AboutComponent() => await Task.Run(() => PartialView(_db.GetMenu()));

		public IActionResult AppComponent() => PartialView();

		public IActionResult ContactComponent() => PartialView();

		public IActionResult IndexComponent() => PartialView();

		public IActionResult LoginComponent() => PartialView();

		public IActionResult RegisterComponent() => PartialView();

		public IActionResult ProfileComponent() => PartialView();

		public IActionResult CartComponent() => PartialView();
	}
}