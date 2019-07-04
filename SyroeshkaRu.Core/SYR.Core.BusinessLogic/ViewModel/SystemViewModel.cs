using SYR.Core.BusinessLogic.Common;
using SYR.Core.BusinessLogic.Helpers;
using System;
using System.Collections.Generic;

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
		public bool Allow { get; set; }
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
		public string OperationType { get; set; }
		public string User { get; set; }

		public DateTime DateTimeIn
		{
			get => DisplayValues.ConvertFromTimestamp(DateIn);
			set => value = DateTimeIn;
		}
	}

	public class PageViewModel
	{
		public int PageNumber { get; }
		public int? TotalPages { get; }

		public PageViewModel(int count, int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		}

		public bool HasPreviousPage => (PageNumber > 1);

		public bool HasNextPage => (PageNumber < TotalPages);
	}

	public class IndexViewModel
	{
		public object ModelObject { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}

	public class ModalViewModel
	{
		public string Type { get; set; }
		public string Title { get; set; }
		public string Data { get; set; }
	}
}