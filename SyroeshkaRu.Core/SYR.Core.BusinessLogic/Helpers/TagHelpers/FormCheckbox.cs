using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;

namespace SYR.Core.BusinessLogic.Helpers.TagHelpers
{
	[HtmlTargetElement("form-checkbox", Attributes = "model, element")]
	public class FormCheckboxTagHelpers : TagHelper
	{
		/// <summary>
		/// TagHelper для checkbox
		/// </summary>
		/// <value>
		/// The model.
		/// </value>
		[HtmlAttributeName("model")]
		// ReSharper disable once MemberCanBePrivate.Global
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public object Model { get; set; }

		/// <summary>
		/// Свойство модели.
		/// <example>
		///	element="IsBool"
		/// </example>
		/// </summary>
		[HtmlAttributeName("element")]
		// ReSharper disable once MemberCanBePrivate.Global
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
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
}