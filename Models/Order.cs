namespace sem3.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public DateTime OrderDate { get; set; }
		public int Status { get; set; }
		public User User { get; set; }
	}
}
