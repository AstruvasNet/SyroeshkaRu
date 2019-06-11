using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Helpers;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.ViewModel;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	[Route("api")]
	public class ApiController : Controller
	{
		private readonly ISyroeshkaRu _db;
		private readonly IEdit _edit;

		public ApiController(ISyroeshkaRu db, IEdit edit)
		{
			_db = db;
			_edit = edit;
		}

		[
			Route("[action]/{id?}"),
			ValidateAntiForgeryToken
		]
		public async Task<string> SetStorage(Guid id)
		{
			HttpContext.Session.SetString("storage", id.ToString());
			return await Task.Run(() => HttpContext.Session.GetString("storage"));
		}

		[
			HttpPut("[action]/{id?}"),
			ValidateAntiForgeryToken
		]
		public async Task<IActionResult> EditStorages(StoragesViewModel model)
		{
			var query = _edit.EditStorages(model);
			if (await ErrorHeplers.ModelState(ModelState, query))
			{
				return await Task.Run(() => Ok(query.ToString().Split("//")[1]));
			}

			return await Task.Run(() => BadRequest(ModelState));
		}

		[
			HttpDelete("[action]/{id?}"),
			ValidateAntiForgeryToken
		]
		public async Task<IActionResult> DeleteStorages(StoragesViewModel model)
		{
			var query = _edit.DeleteStorages(model);
			if (await ErrorHeplers.ModelState(ModelState, query))
			{
				return await Task.Run(() => Ok(query.ToString().Split("//")[1]));
			}

			return await Task.Run(() => BadRequest(ModelState));
		}
	}
}