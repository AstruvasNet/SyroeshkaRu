using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using SYR.Core.BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SYR.Core.BusinessLogic.Filters
{
	[AttributeUsage(AttributeTargets.Class)]
	public class Sequrity : ActionFilterAttribute
	{
		private readonly IAdmin _db = new AdminService();

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var profiles = (ICollection<SequrityProfilesViewModel>)_db.GetSequrityProfiles();
			var route = context.RouteData.Values.FirstOrDefault(i => i.Key == "controller").Value;

			if (context.HttpContext.User.IsInRole("root"))
			{
				context.Result = context.Result;
			}
			else if (profiles.Count(i => i.Name.Contains(route.ToString().ToLower())) != 0)
			{
				ICollection<string> source =
					(from profile in ((SequrityProfilesViewModel)_db.GetSequrityProfiles(route.ToString()))
							.SequrityRoles.Where(i => !i.Allow)
					 from role in context.HttpContext.User.FindAll(ClaimTypes.Role)
					 where role.Value == profile.Roles.Name
					 select role.Value).ToList();

				context.Result = source.Count != 0 ? context.Result : new NotFoundResult();
			}
			else
			{
				context.Result = context.Result;
			}
		}
	}
}