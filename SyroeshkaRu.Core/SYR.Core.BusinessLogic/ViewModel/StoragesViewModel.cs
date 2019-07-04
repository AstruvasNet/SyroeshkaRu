using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class StoragesViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Название", Prompt = "Название")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Поле {0} не должно быть пустым")]
		[Display(Name = "Описание", Prompt = "Описание")]
		public string Description { get; set; }

		[Display(Name = "По умолчанию", Prompt = "По умолчанию")]
		public bool IsDefault { get; set; }

		public string ShortName => $"{Title} ({Description})";

		//public ICollection<ProductsViewModel> Products { get; set; }
		public ICollection<StoragesProductsViewModel> Products { get; set; }

		public ICollection<StoragesCategoriesViewModel> Categories { get; set; }
	}

	public class StoragesProductsViewModel
	{
		private readonly ISyroeshkaRu _db;

		public StoragesProductsViewModel()
		{
			_db = new SyroeshkaRuService();
		}

		public Guid StorageId { get; set; }
		public Guid ProductId { get; set; }

		public ProductsViewModel Product { get; set; }

		public decimal Price { get; set; }
		public decimal Quantity { get; set; }
	}

	public class StoragesCategoriesViewModel
	{
		private readonly ISyroeshkaRu _db;

		public StoragesCategoriesViewModel()
		{
			_db = new SyroeshkaRuService();
		}

		public Guid StorageId { get; set; }
		public ICollection<CategoriesViewModel> Categories => (ICollection<CategoriesViewModel>)_db.GetCategories(StorageId);
	}
}