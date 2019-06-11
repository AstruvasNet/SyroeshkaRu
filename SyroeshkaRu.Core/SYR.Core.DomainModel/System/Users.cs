using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SYR.Core.DomainModel.System
{
	public class Users : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string SecondName { get; set; }
		public bool IsDeleted { get; set; }
		//public Guid LocationId { get; set; }
	}
}
