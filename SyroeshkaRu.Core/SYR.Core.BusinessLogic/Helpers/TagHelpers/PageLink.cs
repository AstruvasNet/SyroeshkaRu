using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SYR.Core.BusinessLogic.ViewModel;
using System.Collections.Generic;

namespace SYR.Core.BusinessLogic.Helpers.TagHelpers
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class PageLinkTagHelper : TagHelper
	{
		private readonly IUrlHelperFactory _urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory helperFactory)
		{
			_urlHelperFactory = helperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		// ReSharper disable once MemberCanBePrivate.Global
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public ViewContext ViewContext { get; set; }

		public PageViewModel PageModel { get; set; }
		public string PageAction { get; set; }

		[HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
		// ReSharper disable once MemberCanBePrivate.Global
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

		private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
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