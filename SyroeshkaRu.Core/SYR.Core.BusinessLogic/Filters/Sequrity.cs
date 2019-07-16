using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.Service;
using SYR.Core.BusinessLogic.ViewModel;

namespace SYR.Core.BusinessLogic.Filters {
	/// <summary>Глобальный фильтр ограничения доступа</summary>
	/// <example>
	///     Если в базе существует параметр с полем названия контроллера, то все роли, связанные с этим параметрам имеют доступ
	///     к данному контроллеру.
	///     Если в базе существует параметр с полем названия метода, то все роли, связанные с этим параметром не имеют доступ к
	///     данному методу
	/// </example>
	[AttributeUsage(AttributeTargets.All)]
	public class Sequrity : ActionFilterAttribute {
		private readonly IAdmin _db = new AdminService();

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var profiles = (ICollection<SequrityProfilesViewModel>) _db.GetSequrityProfiles();
			var controller = context.RouteData.Values.FirstOrDefault(i => i.Key == "controller").Value;
			var action = context.RouteData.Values.FirstOrDefault(i => i.Key == "action").Value;

			if (context.HttpContext.User.IsInRole("root"))
			{
				context.Result = context.Result;
			}
			else if (profiles.Count(i => i.Name.Contains(controller.ToString().ToLower())) != 0)
			{
				ICollection<string> controllerSequrity =
					(from profile in ((SequrityProfilesViewModel) _db.GetSequrityProfiles(controller.ToString()))
							.SequrityRoles
						from role in context.HttpContext.User.FindAll(ClaimTypes.Role)
						where role.Value == profile.Roles.Name
						select role.Value).ToList();

				context.Result = controllerSequrity.Count != 0 ? context.Result : new NotFoundResult();

				if (profiles.Count(i => i.Name.Contains(action.ToString().ToLower())) != 0)
				{
					ICollection<string> actionSequrity =
						(from profile in ((SequrityProfilesViewModel) _db.GetSequrityProfiles(action.ToString()))
								.SequrityRoles
							from role in context.HttpContext.User.FindAll(ClaimTypes.Role)
							where role.Value == profile.Roles.Name
							select role.Value).ToList();

					context.Result = actionSequrity.Count == 0 ? context.Result : new NotFoundResult();
				}
			}
			else
			{
				context.Result = context.Result;
			}
		}
	}
}