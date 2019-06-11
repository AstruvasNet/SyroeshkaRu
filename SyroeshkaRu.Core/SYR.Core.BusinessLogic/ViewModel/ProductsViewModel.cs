using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using System;
using System.Collections.Generic;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class ProductsViewModel
	{
		private readonly ISyroeshkaRu _db;

		public ProductsViewModel()
		{
			_db = new SyroeshkaRuService();
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Keywords { get; set; }
		public string Content { get; set; }
		public bool IsNew { get; set; }

		public ICollection<StoragesProductsViewModel> StoragesProducts { get; set; }
		public ICollection<CategoriesProductsViewModel> CategoriesProducts { get; set; }
	}
}
