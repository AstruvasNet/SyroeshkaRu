using System;

namespace SYR.Core.DomainModel.System
{
	public class Menu
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public int Type { get; set; }
		public Guid? ParentId { get; set; }
		public int? Level { get; set; }
		public Guid SequrityId { get; set; }
		public bool IsDeleted { get; set; }
	}
}