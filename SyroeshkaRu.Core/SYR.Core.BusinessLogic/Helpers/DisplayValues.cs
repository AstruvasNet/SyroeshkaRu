using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SYR.Core.BusinessLogic.Helpers
{
	public static class DisplayValues
	{
		private static readonly ISyroeshkaRu _db = new SyroeshkaRuService();

		private static readonly DateTime originDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		public static string GetEnum(this Enum enumValue)
		{
			return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>()
				.Name;
		}

		public static string GetMenuController(Guid? parentId)
		{
			return _db.GetMenuController(parentId).ToString();
		}

		public static int ConvertToTimestamp(DateTime dateTime)
		{
			var origin = originDateTime;
			var diff = dateTime - origin;
			return Convert.ToInt32(Math.Floor(diff.TotalSeconds));
		}

		public static DateTime ConvertFromTimestamp(int timestamp)
		{
			var origin = originDateTime;
			return origin.AddSeconds(timestamp);
		}
	}
}
