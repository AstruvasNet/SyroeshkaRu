using System;

namespace SYR.Core.DomainModel.Client
{
	public class CategoriesProducts
	{
		public Guid Id { get; set; }
		public Guid CategoryId { get; set; }
		public Categories Category { get; set; }

		public Guid ProductId { get; set; }
		public Products Product { get; set; }
	}
}