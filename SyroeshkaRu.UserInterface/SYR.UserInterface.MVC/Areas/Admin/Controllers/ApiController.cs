using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Helpers;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.ViewModel;
using System;
using System.Threading.Tasks;
using SYR.Core.BusinessLogic.Filters;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers {
	[Route("api")]
	public class ApiController : Controller {
		private readonly IEdit _edit;

		public ApiController(IEdit edit)
		{
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

//		[
//			HttpPut("[action]/{id?}"),
//			ValidateAntiForgeryToken,
//			DbMessage(nameof(EditStorages))
//		]
//		public async Task<IActionResult> EditStorages(StoragesViewModel model)
//		{
//			//var query = _edit.EditStorages(model);
////			if (await ErrorHeplers.ModelState(ModelState, query))
////			{
////				return await Task.Run(() => Ok(query.ToString().Split("//")[1]));
////			}
////			if (ModelState.IsValid)
////			{
//				return await Task.Run(() => Ok(_edit.EditStorages(model)));
////			}
////
////			return await Task.Run(() => BadRequest(ModelState));
//		}

		[
			HttpPost,
			ValidateAntiForgeryToken
		]
		public async Task<IActionResult> EditStorages(StoragesViewModel model)
		{
			return await Task.Run(() => Ok(_edit.EditStorages(model)));
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

		[HttpPost("[action]")]
		public async Task<string> GetTest()
		{
			return await Task.Run(() => "Request: Ok!");
		}
	}
}