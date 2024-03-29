﻿using System.ComponentModel.DataAnnotations;

namespace SYR.Core.BusinessLogic.Common
{
	public enum ModelError
	{
		[Display(Name = "Не должно быть пустым")]
		Empty,

		[Display(Name = "Уже существует")]
		Distinct,

		[Display(Name = "Имеет неверный формат")]
		RegularExpression
	}

	public enum AccessType
	{
		Admin,
		Site
	}

	public enum ResourceType
	{
		Admin,
		Site
	}

	public enum SiteType
	{
		Resource,
		Menu
	}

	public enum ItemType
	{
		Storage
	}

	public enum OperationType
	{
		Adding,
		Updating,
		Removed,
		Deleted
	}
}