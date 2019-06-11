using System;

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
}