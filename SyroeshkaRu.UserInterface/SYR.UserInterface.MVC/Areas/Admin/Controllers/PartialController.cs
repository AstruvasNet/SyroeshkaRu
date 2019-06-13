using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SYR.Core.BusinessLogic.Helpers;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Core.BusinessLogic.ViewPatterns;

namespace SYR.UserInterface.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PartialController : Controller
	{
		private readonly ISyroeshkaRu _db;

		public PartialController(ISyroeshkaRu db)
		{
			_db = db;
		}

		[HttpPost("/partial/menu")]
		public IActionResult Menu()
		{
			var test = _db.GetMainMenu();
			return PartialView(test as ICollection<MenuViewModel>);
		}

		[HttpGet("/[controller]/[action]")]
		public async Task<IActionResult> EditStorages(StoragesViewModel model)
		{
			return await Task.Run(() => PartialView(_db.GetStorages(model.Id)));
		}

		[HttpGet("[controller]/[action]/{type?}/{id?}")]
		public async Task<IActionResult> Confirm(int type, Guid id)
		{
			var pattern = new WindowStructure();
			var model = _db.GetStorages(id) as StoragesViewModel;
			Debug.Assert(model != null, nameof(model) + " != null");
			switch (type)
			{
				case 0:
					pattern.Data = model;
					pattern.Header = $"Удаление склада {model.Title}";
					pattern.Content = $"Внимание! После подтверждения удаления склад {model.Title} и всё, что с ним связано будет удалено";
					break;
				default:
					pattern.Data = null;
					pattern.Header = "Ошибка";
					pattern.Content = "Паттерн не найден";
					break;
			}
			return await Task.Run(() => PartialView(pattern));
		}
	}
}