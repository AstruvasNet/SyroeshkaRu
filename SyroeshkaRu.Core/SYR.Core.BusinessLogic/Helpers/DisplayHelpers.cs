﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using SYR.Core.BusinessLogic.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SYR.Core.BusinessLogic.Helpers
{
	public class DisplayHelpers
	{
		private static readonly IAdmin _dbAdmin = new AdminService();

		public static IEnumerable<SelectListItem> RolesCollection()
		{
			return ((ICollection<RolesViewModel>)_dbAdmin.GetRoles()).Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id
			}).OrderBy(m => m.Value);
		}

		public static IEnumerable<SelectListItem> StoragesCollection(ISession _session)
		{
			return ((ICollection<StoragesViewModel>)_dbAdmin.GetStorages()).Select(i => new SelectListItem
			{
				Text = i.Title,
				Value = i.Id.ToString(),
				Selected = string.IsNullOrEmpty(_session.GetString("storage"))
					? i.IsDefault : i.Id.ToString() == _session.GetString("storage")
			}).OrderBy(m => m.Value);
		}

		public static ICollection<string> RoleId(string user)
		{
			return (ICollection<string>)_dbAdmin.GetUserRoles(user);
		}

		public static string SequrityProfileName(Guid id)
		{
			return ((SequrityProfilesViewModel)_dbAdmin.GetSequrityProfiles(id)).Name;
		}
	}
}
