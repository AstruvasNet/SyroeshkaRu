using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SYR.Core.BusinessLogic.Helpers.TagHelpers
{
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
}
