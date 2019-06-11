using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SYR.Core.DomainModel.System
{
	public class Roles : IdentityRole
	{
		public string Content { get; set; }
		public ICollection<SequrityRoles> SequrityRoles { get; set; }
	}
}
