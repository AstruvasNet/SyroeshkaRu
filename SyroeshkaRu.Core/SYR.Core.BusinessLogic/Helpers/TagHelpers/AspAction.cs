using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SYR.Core.BusinessLogic.Interface;
using SYR.Core.BusinessLogic.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SYR.Core.BusinessLogic.Helpers.TagHelpers
{
	[HtmlTargetElement(Attributes = "asp-action")]
	public class AspActionTagHelpers : TagHelper
	{
		private readonly IAdmin _db;

		public AspActionTagHelpers(IAdmin db)
		{
			_db = db;
		}

		[HtmlAttributeName("asp-action")]
		public string AspAction { get; set; }

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var profiles = (ICollection<SequrityProfilesViewModel>)_db.GetSequrityProfiles();

			if (ViewContext.HttpContext.User.IsInRole("root"))
			{
				output.Content = output.Content;
			}
			else if (profiles.Count(i => i.Name.Contains(AspAction)) != 0)
			{
				ICollection<string> source =
					(from profile in ((SequrityProfilesViewModel)_db.GetSequrityProfiles(AspAction))
							.SequrityRoles.Where(i => !i.Allow)
					 from role in ViewContext.HttpContext.User.FindAll(ClaimTypes.Role)
					 where role.Value == profile.Roles.Name
					 select role.Value).ToList();
				if (source.Count != 0) return;
				output.TagName = null;
				output.Content.SetContent(null);
			}
			else
			{
				output.Content = output.Content;
			}
		}
	}
}