namespace sem3.Models
{
	public class ServiceDetail
	{
		public int Id { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public int ServiceId { get; set; }
		public Service Services { get; set; }
	}
}
