using System.ComponentModel.DataAnnotations.Schema;

namespace SYR.Utilites.Core.DomainModel.Model
{
	[Table("node")]
	public class Node
	{
		[Column("nid")]
		public int Id { get; set; }

		[Column("vid")]
		public int SystemId { get; set; }

		[Column("cid")]
		public int ParentId { get; set; }

		[Column("title")]
		public string Title { get; set; }

		[Column("type")]
		public string Type { get; set; }

		[Column("new")]
		public int IsNew { get; set; }
	}
}