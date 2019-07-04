using System;

namespace SYR.Core.DomainModel.Client
{
	public class StoragesProducts
	{
		public Guid ProductId { get; set; }
		public Products Product { get; set; }

		public Guid StorageId { get; set; }
		public Storages Storage { get; set; }

		public decimal Price { get; set; }
		public decimal Quantity { get; set; }
	}
}