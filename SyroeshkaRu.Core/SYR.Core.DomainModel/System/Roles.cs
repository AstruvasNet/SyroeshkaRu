using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SYR.Core.DomainModel.System
{
	public class Roles : IdentityRole
	{
		public string Content { get; set; }
		public ICollection<SequrityRoles> SequrityRoles { get; set; }
	}
}