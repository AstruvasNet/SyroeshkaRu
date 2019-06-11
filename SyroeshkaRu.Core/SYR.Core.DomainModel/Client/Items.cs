using System;
using System.Collections.Generic;
using System.Text;

namespace SYR.Core.DomainModel.Client
{
	public class Items
	{
		public Guid Id { get; set; }
		public int CreateDate { get; set; }
		public int? ShelfLife { get; set; }
		public Products Product { get; set; }
	}
}
