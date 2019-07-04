using System;
using System.Collections.Generic;

namespace SYR.Core.DomainModel.Client
{
	public class Categories
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string Description { get; set; }
		public string Keywords { get; set; }
		public int Level { get; set; }
		public Guid? ParentId { get; set; }
		public bool IsDeleted { get; set; }
		public ICollection<CategoriesProducts> CategoriesProducts { get; set; }
		public ICollection<StoragesCategories> StoragesCategories { get; set; }

		public Categories()
		{
			CategoriesProducts = new List<CategoriesProducts>();
			StoragesCategories = new List<StoragesCategories>();
		}
	}
}