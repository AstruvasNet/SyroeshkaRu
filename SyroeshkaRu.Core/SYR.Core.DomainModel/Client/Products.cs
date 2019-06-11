using System;
using System.Collections.Generic;
using System.Text;

namespace SYR.Core.DomainModel.Client
{
	public class Products
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Keywords { get; set; }
		public string Content { get; set; }
		public bool IsNew { get; set; }
		public bool IsDeleted { get; set; }
		public References Reference { get; set; }
		public ICollection<StoragesProducts> StoragesProducts { get; set; }
		public ICollection<Items> Items { get; set; }
		public ICollection<CategoriesProducts> CategoriesProducts { get; set; }

		public Products()
		{
			StoragesProducts = new List<StoragesProducts>();
			CategoriesProducts = new List<CategoriesProducts>();
		}
	}
}
