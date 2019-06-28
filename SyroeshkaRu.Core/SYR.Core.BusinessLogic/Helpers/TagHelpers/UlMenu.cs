using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SYR.Core.BusinessLogic.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace SYR.Core.BusinessLogic.Helpers.TagHelpers
{
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
}
