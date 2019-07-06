using System;
using System.Collections.Generic;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class ProductsViewModel
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public Guid Id { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Name { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Description { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Keywords { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Content { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public bool IsNew { get; set; }

		public ICollection<StoragesProductsViewModel> StoragesProducts { get; set; }
		public ICollection<CategoriesProductsViewModel> CategoriesProducts { get; set; }
	}
}