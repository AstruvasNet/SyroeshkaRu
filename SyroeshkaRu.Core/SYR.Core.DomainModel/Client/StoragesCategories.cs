using System;

namespace SYR.Core.DomainModel.Client
{
	public class StoragesCategories
	{
		public Guid CategoryId { get; set; }
		public Categories Category { get; set; }

		public Guid StorageId { get; set; }
		public Storages Storage { get; set; }
	}
}