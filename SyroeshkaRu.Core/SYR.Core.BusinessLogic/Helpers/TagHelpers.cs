using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using SYR.Core.BusinessLogic.ViewModel;
using SYR.Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
			output.Attributes.SetAttribute("class", "form-hidden-element");

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
						var input = new TagBuilder("input") { TagRenderMode = TagRenderMode.SelfClosing };
						input.MergeAttribute("type", "hidden");
						input.MergeAttribute("id", item.Name);
						input.MergeAttribute("name", item.Name);
						input.MergeAttribute("value", item.GetValue(Model, null).ToString());
						output.Content.AppendHtml(input);
					}
				}
			}
			else
			{
				foreach (var item in collection)
				{
					var input = new TagBuilder("input") { TagRenderMode = TagRenderMode.SelfClosing };
					input.MergeAttribute("type", "hidden");
					input.MergeAttribute("id", item.Name);
					input.MergeAttribute("name", item.Name);
					input.MergeAttribute("value", item.GetValue(Model, null).ToString());
					output.Content.AppendHtml(input);
				}
			}
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

			var collection = Model.GetType().GetProperties()
				.Where(item => item.PropertyType.Namespace != "System.Collections.Generic").ToList();

			foreach (var item in collection.Where(i => i.Name.Contains(Element)))
			{
				output.Attributes.Add("id", item.Name);
				output.Attributes.Add("name", item.Name);
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

	[HtmlTargetElement("ul-menu", Attributes = "model")]
	public class UlMenuTagHelpers : TagHelper
	{
		public UlMenuTagHelpers()
		{
			Value = null;
			AHref = null;
			UlClass = null;
			LiClass = null;
			Second = null;
		}

		[HtmlAttributeName("model")]
		public ICollection<MenuViewModel> Model { get; set; }

		[HtmlAttributeName("value")]
		public string Value { get; set; }

		[HtmlAttributeName("ul-class")]
		public string UlClass { get; set; }

		[HtmlAttributeName("li-class")]
		public string LiClass { get; set; }

		[HtmlAttributeName("a-href")]
		public string AHref { get; set; }

		[HtmlAttributeName("second")]
		public string Second { get; set; }

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var controller = ViewContext.RouteData.Values["controller"].ToString().ToLower();
			var action = ViewContext.RouteData.Values["action"].ToString().ToLower();
			output.TagName = "ul";
			output.Attributes.Add("class", UlClass);

			if (Second == null)
			{
				foreach (var item in Model.Where(i => i.ParentId == null).OrderBy(i => i.Level))
				{
					var li = new TagBuilder("li");
					var a = new TagBuilder("a");
					li.AddCssClass(LiClass);

					a.InnerHtml.Append(item.Title);
					a.AddCssClass("nav-link");

					if (item.Name.Contains(controller))
					{
						li.AddCssClass("active");
					}
					li.InnerHtml.AppendHtml(a);
					a.MergeAttribute("href",
						$"/cp/{(item.ParentId != null ? DisplayValues.GetMenuController(item.ParentId) : item.Name)}/{(Model.Count(i => i.ParentId.ToString().Contains(item.Id.ToString())) != 0 ? Model.Where(i => i.ParentId == item.Id).FirstOrDefault(i => i.Level == 1)?.Name : item.Name)}");
					output.Content.AppendHtml(li);
				}
			}
			else
			{
				var parentId = Model.FirstOrDefault(i => i.Name.Contains(Second.ToLower()))?.Id;
				foreach (var item in Model.Where(i => i.ParentId == parentId).OrderBy(i => i.Level))
				{
					var li = new TagBuilder("li");
					var a = new TagBuilder("a");

					if (action == "index")
						action = item.Name;

					li.AddCssClass(LiClass);
					if (item.Name.Contains(action))
					{
						li.AddCssClass("active");
					}
					a.InnerHtml.Append(item.Title);
					a.AddCssClass("nav-link");
					li.InnerHtml.AppendHtml(a);
					a.MergeAttribute("href",
						$"/cp/{(item.ParentId != null ? DisplayValues.GetMenuController(item.ParentId) : item.Name)}/{(Model.Count(i => i.ParentId.ToString().Contains(item.Id.ToString())) != 0 ? Model.Where(i => i.ParentId == item.Id).FirstOrDefault(i => i.Level == 1)?.Name : item.Name)}");
					output.Content.AppendHtml(li);
				}
			}
		}
	}

	public class PageLinkTagHelper : TagHelper
	{
		private readonly IUrlHelperFactory _urlHelperFactory;
		public PageLinkTagHelper(IUrlHelperFactory helperFactory)
		{
			_urlHelperFactory = helperFactory;
		}
		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }
		public PageViewModel PageModel { get; set; }
		public string PageAction { get; set; }

		[HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
		public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
			output.TagName = "div";

			TagBuilder tag = new TagBuilder("ul");
			tag.AddCssClass("pagination");

			TagBuilder currentItem = CreateTag(PageModel.PageNumber, urlHelper);

			if (PageModel.HasPreviousPage)
			{
				TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper);
				tag.InnerHtml.AppendHtml(prevItem);
			}

			tag.InnerHtml.AppendHtml(currentItem);

			if (PageModel.HasNextPage)
			{
				TagBuilder nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper);
				tag.InnerHtml.AppendHtml(nextItem);
			}
			output.Content.AppendHtml(tag);
		}

		TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
		{
			TagBuilder item = new TagBuilder("li");
			TagBuilder link = new TagBuilder("a");
			if (pageNumber == PageModel.PageNumber)
			{
				item.AddCssClass("active");
			}
			else
			{
				PageUrlValues["page"] = pageNumber;
				link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
			}
			link.InnerHtml.Append(pageNumber.ToString());
			item.InnerHtml.AppendHtml(link);
			return item;
		}
	}
}
