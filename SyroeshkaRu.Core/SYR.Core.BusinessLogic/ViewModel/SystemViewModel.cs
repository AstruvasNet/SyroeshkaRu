using SYR.Core.BusinessLogic.Common;
using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SYR.Core.BusinessLogic.Helpers;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class SequrityProfilesViewModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Name { get; set; }
		public ICollection<SequrityRolesViewModel> SequrityRoles { get; set; }
	}

	public class SequrityRolesViewModel
	{
		public Guid ResourceId { get; set; }
		public string RoleId { get; set; }
		public RolesViewModel Roles { get; set; }
	}

	public class RolesViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}

	public class MenuViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public Guid? ParentId { get; set; }
		public int? Level { get; set; }
		public SiteType Type { get; set; }
		public Guid SequrityId { get; set; }

	}

	public class HistoryViewModel
	{
		public Guid Id { get; set; }
		public int DateIn { get; set; }
		public string Item { get; set; }

		public DateTime DateTimeIn
		{
			get => DisplayValues.ConvertFromTimestamp(DateIn);
			set => value = DateTimeIn;
		}
	}
}
