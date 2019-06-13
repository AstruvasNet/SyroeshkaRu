using System;
using System.Collections;
using System.Collections.Generic;

namespace SYR.Core.BusinessLogic.ViewPatterns
{
	public struct WindowStructure
	{
		public string Content { get; set; }
		public string Header { get; set; }
		public object Data { get; set; }

		public WindowStructure(object data, string content, string header)
		{
			Data = data;
			Content = content;
			Header = header;
		}
	}

	public struct FormHidden
	{
		//public string Key { get; set; }
		//public string Value { get; set; }

		//public FormHidden(string key, string value)
		//{
		//	Key = key;
		//	Value = value;
		//}
		public IDictionary<string, string> Attributes { get; set; }

		public FormHidden(IDictionary<string, string> attributes)
		{
			Attributes = attributes;
		}
	}
}