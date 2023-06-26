namespace sem3.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public string Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string SubDescription { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdateAt { get; set; }
		public int Userid { get; set; }
		public User User { get; set; }
	}
}
