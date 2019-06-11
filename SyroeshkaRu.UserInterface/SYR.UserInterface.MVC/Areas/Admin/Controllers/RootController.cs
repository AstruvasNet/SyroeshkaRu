using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.DomainModel.System;
using System.Threading.Tasks;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	[
		Area("Admin"),
		Route("cp/root"),
		Authorize(Policy = "root")
	]
	public class RootController : Controller
	{
		private readonly UserManager<Users> _userManager;
		private readonly IAdmin _db;
		private readonly ISyroeshkaRu _repo;

		public RootController(UserManager<Users> userManager, IAdmin db, ISyroeshkaRu repo)
		{
			_userManager = userManager;
			_db = db;
			_repo = repo;
		}

		[Route("users/{id?}")]
		public async Task<IActionResult> Index(string id)
		{
			if (!string.IsNullOrEmpty(id))
				return await Task.Run(() => View("~/Areas/Admin/Views/Root/Users.cshtml", _db.GetUsers(id)));
			return await Task.Run(() => View(_userManager));
		}

		[Route("[action]/{id?}")]
		public async Task<IActionResult> Storages(Guid? id)
		{
			if (!string.IsNullOrEmpty(id.ToString()))
				return await Task.Run(() => View("~/Areas/Admin/Views/Root/Storage.cshtml",_repo.GetStorages(id)));
			return await Task.Run(() => View(_repo.GetStorages()));
		}

		[Route("[action]/{id?}")]
		public async Task<IActionResult> Sequrity(Guid? id)
		{
			if(!string.IsNullOrEmpty(id.ToString()))
				return await Task.Run(() => View("~/Areas/Admin/Views/Root/Profile.cshtml", _db.GetSequrityProfiles(id)));
			return await Task.Run(() => View(_db.GetSequrityProfiles()));
		}

		[Route("[action]/{id?}")]
		public async Task<IActionResult> History()
		{
			return await Task.Run(() => View(_db.GetHistory()));
		}
	}
}