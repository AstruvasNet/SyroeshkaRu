using System;
using System.Collections.Generic;
using System.Text;

namespace SYR.Core.DomainModel.Client
{
	public class Storages
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsDefault { get; set; }
		public bool IsDeleted { get; set; }
		//TODO Переименовать в Products и Storages
		public ICollection<StoragesProducts> Products { get; set; }
		public ICollection<StoragesCategories> Categories { get; set; }

		public Storages()
		{
			Products = new List<StoragesProducts>();
			Categories = new List<StoragesCategories>();
		}
	}
}
