using System;

namespace BlogApi.Models
{
	public class BlogItemDTO
	{
		public long Id { get; set; }
		public string? Name{ get; set; }
		public bool IsComplete { get; set; }
		public string? Secret { get; set; }
	
	}
}