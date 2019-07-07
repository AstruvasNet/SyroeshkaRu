using System;
using System.Collections.Generic;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class SequrityProfilesViewModel
	{
		public Guid Id { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Title { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Name { get; set; }
		// ReSharper disable once CollectionNeverUpdated.Global
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public ICollection<SequrityRolesViewModel> SequrityRoles { get; set; }
	}

	public class SequrityRolesViewModel
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public bool Allow { get; set; }
		public Guid ResourceId { get; set; }
		public string RoleId { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public RolesViewModel Roles { get; set; }
	}

	public class RolesViewModel
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Id { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Name { get; set; }
	}

	public class SequrityViewModel
	{
		public ICollection<Type> Controllers { get; set; }
		public ICollection<Type> Methods { get; set; }
	}
}
