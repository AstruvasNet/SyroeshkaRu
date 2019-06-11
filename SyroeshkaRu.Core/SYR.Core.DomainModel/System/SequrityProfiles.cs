using System;
using System.Collections.Generic;

namespace SYR.Core.DomainModel.System
{
	public class SequrityProfiles
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Name { get; set; }
		public ICollection<SequrityRoles> SequrityRoles { get; set; }
	}
}
