using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using SYR.Core.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SYR.Core.BusinessLogic.Helpers
{
	[HtmlTargetElement(Attributes = "isAuth")]
	// ReSharper disable once UnusedMember.Global
	public class IsAuthTagHelpers : TagHelper
	{
		private readonly IHttpContextAccessor _user;
		private readonly ModelContext _db;

		public IsAuthTagHelpers(IHttpContextAccessor user, ModelContext db)
		{
			_user = user;
			_db = db;
		}

		[HtmlAttributeName("isAuth")]
		public string IsAuth { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			Task.Run(() =>
			{
				bool user = _user.HttpContext.User.Identity.IsAuthenticated;
				ICollection<string> resource = new List<string>();
				foreach (var x in _user.HttpContext.User.FindAll(ClaimTypes.Role))
				{
					string roleId = _db.Roles.Where(i => i.Name == x.Value).Select(i => i.Id).FirstOrDefault();
					if (user)
					{
						var count = _db.SequrityProfiles.Include(inc => inc.SequrityRoles).ThenInclude(inc => inc.Roles)
							.Where(i => i.Name == IsAuth);
						if (count.Count() != 0)
							foreach (var m in count)
							{
								foreach (var i in m.SequrityRoles.Where(z =>
									z.RoleId.GetHashCode() == roleId?.GetHashCode()))
								{
									resource.Add(i.RoleId);
								}
							}
					}
					else
					{
						output.TagName = null;
						output.Content.SetContent(null);
					}
				}

				if (resource.Count == 0)
				{
					output.TagName = null;
					output.Content.SetContent(null);
				}
				else
				{
					output.Attributes.Add("class", "Ok");
				}
			}).Wait();
		}
	}

	[HtmlTargetElement("form-hidden", Attributes = "model")]
	public class FormHiddenTagHelpers : TagHelper
	{

		public FormHiddenTagHelpers()
		{
			Items = "";
		}

		[HtmlAttributeName("items")]
		public string Items { get; set; }

		[HtmlAttributeName("model")]
		public object Model { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "div";
			output.Attributes.SetAttribute("class", "form-hidden-elements");

			var list = "";
			var attr = Items.Split(",").ToList();
			var collection = Model.GetType().GetProperties()
				.Where(item => item.PropertyType.Namespace != "System.Collections.Generic").ToList();
			ICollection<PropertyInfo> outputs;

			if (!string.IsNullOrEmpty(nameof(Items)))
			{
				for (var i = attr.Count - 1; i >= 0; i--)
				{
					outputs = collection.Where(item => item.Name.Contains(attr[i])).ToList();
					foreach (var item in outputs)
					{
						list +=
							$"<input type=\"hidden\" id=\"{item.Name}\" name=\"{item.Name}\" value=\"{item.GetValue(Model, null)}\"/>";
					}
				}
			}
			else
			{
				outputs = collection;
				foreach (var item in outputs)
				{
					list +=
						$"<input type=\"hidden\" id=\"{item.Name}\" name=\"{item.Name}\" value=\"{item.GetValue(Model, null)}\"/>";
				}
			}

			output.Content.SetHtmlContent(list);

		}
	}
}
