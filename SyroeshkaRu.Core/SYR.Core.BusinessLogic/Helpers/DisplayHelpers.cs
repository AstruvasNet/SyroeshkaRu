using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using SYR.Core.BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYR.Core.BusinessLogic.Helpers
{
	public static class DisplayHelpers
	{
		private static readonly IAdmin DbAdmin = new AdminService();

		public static IEnumerable<SelectListItem> RolesCollection()
		{
			return ((ICollection<RolesViewModel>)DbAdmin.GetRoles()).Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id
			}).OrderBy(m => m.Value);
		}

		public static IEnumerable<SelectListItem> StoragesCollection(ISession session)
		{
			return ((ICollection<StoragesViewModel>)DbAdmin.GetStorages()).Select(i => new SelectListItem
			{
				Text = i.Title,
				Value = i.Id.ToString(),
				Selected = string.IsNullOrEmpty(session.GetString("storage"))
					? i.IsDefault : i.Id.ToString() == session.GetString("storage")
			}).OrderBy(m => m.Value);
		}

		public static ICollection<string> RoleId(string user)
		{
			return (ICollection<string>)DbAdmin.GetUserRoles(user);
		}

		public static string SequrityProfileName(Guid id)
		{
			return ((SequrityProfilesViewModel)DbAdmin.GetSequrityProfiles(id)).Name;
		}
	}
}