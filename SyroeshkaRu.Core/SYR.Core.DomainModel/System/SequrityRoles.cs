using System;

namespace SYR.Core.DomainModel.System
{
	public class SequrityRoles
	{
		public Guid SequrityProfileId { get; set; }
		public SequrityProfiles SequrityProfile { get; set; }

		public string RoleId { get; set; }
		public Roles Roles { get; set; }
	}
}