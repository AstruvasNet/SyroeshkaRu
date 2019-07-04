using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SYR.Core.BusinessLogic.Common
{
	public class AccessRequirement : IAuthorizationRequirement
	{
		public AccessRequirement(string profile)
		{
			Profile = profile;
		}

		public string Profile { get; set; }
	}

	public class AccessHandler : AuthorizationHandler<AccessRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessRequirement requirement)
		{
			if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
			{
				return Task.FromResult(0);
			}

			var roles = context.User.FindAll(
				c => c.Type == ClaimTypes.Role);

			foreach (var i in roles)
			{
				if (i.Value == requirement.Profile)
				{
					context.Succeed(requirement);
				}
			}
			return Task.FromResult(0);
		}
	}
}