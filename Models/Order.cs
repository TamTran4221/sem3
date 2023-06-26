namespace sem3.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime OrderDate { get; set; }
		public byte Status { get; set; }
		public User User { get; set; }
	}
}
