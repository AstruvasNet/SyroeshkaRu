using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.ViewModel;
using System.Threading.Tasks;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PartialController : Controller
	{
		private readonly IAdmin _db;

		public PartialController(IAdmin db)
		{
			_db = db;
		}

		[HttpGet("/[controller]/[action]")]
		public async Task<IActionResult> EditStorages(StoragesViewModel model)
		{
			return await Task.Run(() => PartialView(_db.GetStorages(model.Id)));
		}

		[HttpPost("[controller]/[action]")]
		public async Task<IActionResult> GetModal(int type)
		{
			return await Task.Run(() => PartialView(type));
		}

		[HttpPost("[controller]/[action]")]
		public async Task<IActionResult> GetLoginForm()
		{
			return await Task.Run(PartialView);
		}
	}
}