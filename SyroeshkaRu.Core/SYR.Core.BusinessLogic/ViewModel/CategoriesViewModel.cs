using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using System;
using System.Collections.Generic;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class CategoriesViewModel
	{
		private readonly ISyroeshkaRu _db;

		public CategoriesViewModel() => _db = new SyroeshkaRuService();

		private Guid Id => default;

		public string Title { get; set; }
		//public ICollection<ProductsViewModel> Products => (ICollection<ProductsViewModel>)_db.GetCategoryProducts(Id);
	}

	public class CategoriesProductsViewModel
	{
		private readonly ISyroeshkaRu _db;

		private CategoriesProductsViewModel()
		{
			_db = new SyroeshkaRuService();
		}

		private Guid CategoryId => default;

		public Guid ProductId { get; set; }
		public ICollection<CategoriesViewModel> Categories => (ICollection<CategoriesViewModel>)_db.GetCategories(CategoryId);
		public CategoriesViewModel Category => (CategoriesViewModel)_db.GetCategories(CategoryId);
		public ProductsViewModel Product { get; set; }
		public ICollection<ProductsViewModel> Products => (ICollection<ProductsViewModel>)_db.GetProducts(CategoryId);
	}
}