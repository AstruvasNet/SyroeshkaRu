using SYR.Core.BusinessLogic.Common;
using SYR.Core.BusinessLogic.Helpers;
using System;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class MenuViewModel
	{
		public Guid Id { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Name { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Title { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public Guid? ParentId { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public int? Level { get; set; }
		public SiteType Type { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public Guid SequrityId { get; set; }
	}

	// ReSharper disable once ClassNeverInstantiated.Global
	public class HistoryViewModel
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public Guid Id { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Local
		private int DateIn { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string Item { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string OperationType { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public string User { get; set; }

		public DateTime DateTimeIn
		{
			get => DisplayValues.ConvertFromTimestamp(DateIn);
			// ReSharper disable once RedundantAssignment
			set => value = DateTimeIn;
		}
	}

	public class PageViewModel
	{
		public int PageNumber { get; }
		private int? TotalPages { get; }

		public PageViewModel(int count, int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		}

		public bool HasPreviousPage => (PageNumber > 1);

		public bool HasNextPage => (PageNumber < TotalPages);
	}

	public class PaginationViewModel
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public object ModelObject { get; set; }
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public PageViewModel PageObject { get; set; }
	}
}