using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using SYR.Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SYR.Core.BusinessLogic.Helpers
{
	[HtmlTargetElement(Attributes = "isAuth")]
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
			}).Wait();
		}
	}

	[HtmlTargetElement("form-hidden", Attributes = "model")]
	public class FormHiddenTagHelpers : TagHelper
	{

		public FormHiddenTagHelpers()
		{
			Elements = "";
		}

		/// <summary>
		/// Модель.
		/// <example>
		///	model="Model"
		/// </example>
		/// </summary>
		[HtmlAttributeName("model")]
		public object Model { get; set; }

		/// <summary>
		/// Перечисление свойств модели.
		/// elements="Id,Name,Title"
		/// </summary>
		[HtmlAttributeName("elements")]
		public string Elements { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "div";
			output.Attributes.SetAttribute("class", "form-hidden-elements");

			var list = "";
			var attr = Elements.Split(",").ToList();
			var collection = Model.GetType().GetProperties()
				.Where(item => item.PropertyType.Namespace != "System.Collections.Generic").ToList();

			if (!string.IsNullOrEmpty(nameof(Elements)))
			{
				for (var i = attr.Count - 1; i >= 0; i--)
				{
					ICollection<PropertyInfo> outputs = collection.Where(item => item.Name.Contains(attr[i])).ToList();
					foreach (var item in outputs)
					{
						list +=
							$"<input type=\"hidden\" id=\"{item.Name}\" name=\"{item.Name}\" value=\"{item.GetValue(Model, null)}\"/>";
					}
				}
			}
			else
			{
				foreach (var item in collection)
				{
					list +=
						$"<input type=\"hidden\" id=\"{item.Name}\" name=\"{item.Name}\" value=\"{item.GetValue(Model, null)}\"/>";
				}
			}

			output.Content.SetHtmlContent(list);

		}
	}

	[HtmlTargetElement("form-checkbox", Attributes = "model, element")]
	public class FormCheckboxTagHelpers : TagHelper
	{
		/// <summary>
		/// Модель.
		/// <example>
		///	model="Model"
		/// </example>
		/// </summary>
		[HtmlAttributeName("model")]
		public object Model { get; set; }

		/// <summary>
		/// Свойство модели.
		/// <example>
		///	element="IsBool"
		/// </example>
		/// </summary>
		[HtmlAttributeName("element")]
		public string Element { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "input";
			output.Attributes.Add("type", "checkbox");

			if (Model == null)
			{
				output.Attributes.Add("name", Element);
				return;
			}

			//if (context.AllAttributes.FirstOrDefault(i => i.Name == Element)?.Value.ToString() == "on")
			//{

			//}

			var collection = Model.GetType().GetProperties()
				.Where(item => item.PropertyType.Namespace != "System.Collections.Generic").ToList();

			foreach (var item in collection.Where(i => i.Name.Contains(Element)))
			{
				output.Attributes.Add("id", item.Name);
				output.Attributes.Add("name", item.Name);
			}

			foreach (var item in collection.Where(item => item.Name.Contains(Element)))
			{
				if (!Convert.ToBoolean(item.GetValue(Model, null)))
				{
					output.Attributes.RemoveAll("checked");
				}
				else
				{
					output.Attributes.Add("checked", "checked");
					output.Attributes.Add("disabled", "disabled");
				}
			}
		}
	}
}
