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

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public int DateTime { get; set; }

		public string ShortName => $"{Title} ({Description})";

		//public ICollection<ProductsViewModel> Products { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public ICollection<StoragesProductsViewModel> Products { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public ICollection<StoragesCategoriesViewModel> Categories { get; set; }
	}

	public class StoragesProductsViewModel
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public Guid StorageId { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public Guid ProductId { get; set; }

		public ProductsViewModel Product { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public decimal Price { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public decimal Quantity { get; set; }
	}

	public class StoragesCategoriesViewModel
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		// ReSharper disable once MemberCanBePrivate.Global
		public Guid StorageId { get; set; }
	}
}