using System;
using System.Collections.Generic;
using System.Text;

namespace SYR.Core.DomainModel.Client
{
	public class CategoriesProducts
	{
		public Guid CategoryId { get; set; }
		public Categories Category { get; set; }

		public Guid ProductId { get; set; }
		public Products Product { get; set; }
	}
}
