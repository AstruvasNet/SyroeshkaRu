using Newtonsoft.Json;

namespace SYR.Core.BusinessLogic.ViewModel
{
	public class MessagesViewModel
	{
		public bool Type { get; set; }

		[JsonProperty("responseText")]
		public string ResponseText { get; set; }

		public string Message { get; set; }
	}
}