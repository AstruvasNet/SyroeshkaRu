using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Filters;
using SYR.Core.BusinessLogic.Interface;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	[
		Area("Admin"),
		Route("cp/root"),
		Sequrity
	]
	public class RootController : Controller
	{
		private readonly IAdmin _db;

		public RootController(IAdmin db)
		{
			_db = db;
		}

		[Route("users/{id?}")]
		public async Task<IActionResult> Index(string id, int? page = 1)
		{
			if (!string.IsNullOrEmpty(id))
				return await Task.Run(() => View("~/Areas/Admin/Views/Root/Users.cshtml", _db.GetUsers(id)));
			if (page != null)
				return await Task.Run(() => View(_db.GetUsers((int)page, 10)));
			return await Task.Run(() => View(_db.GetUsers()));
		}

		[Route("[action]/{id?}")]
		public async Task<IActionResult> Storages(Guid? id, int? page = 1)
		{
			if (!string.IsNullOrEmpty(id.ToString()))
				return await Task.Run(() => View("~/Areas/Admin/Views/Root/Storage.cshtml", _db.GetStorages(id)));
			if (page != null)
				return await Task.Run(() => View(_db.GetStorages((int)page, 3)));
			return await Task.Run(() => View(_db.GetStorages()));
		}

		[Route("[action]/{id?}"), Sequrity]
		public async Task<IActionResult> Products(Guid? id, int? page = 1)
		{
			return await Task.Run(View);
		}

		[Route("[action]/{id?}"), Sequrity]
		public async Task<IActionResult> Sequrity(Guid? id)
		{
			if (!string.IsNullOrEmpty(id.ToString()))
				return await Task.Run(() => View("~/Areas/Admin/Views/Root/Profile.cshtml", _db.GetSequrityProfiles(id)));
			return await Task.Run(() => View(_db.GetSequrityProfiles(Assembly.GetEntryAssembly())));
		}

		[Route("[action]/{id?}"), Sequrity]
		public async Task<IActionResult> History(Guid? id, int? page = 1)
		{
			if (page != null)
			{
				return await Task.Run(() => View(_db.GetHistory((int)page, 10)));
			}

			return await Task.Run(View);
		}
	}
}