using System;

namespace SYR.Core.DomainModel.System
{
	public class History
	{
		public Guid Id { get; set; }
		public int OperationType { get; set; }
		public int ItemType { get; set; }
		public int DateIn { get; set; }
		public Guid ItemId { get; set; }
		public Guid UserId { get; set; }
	}
}