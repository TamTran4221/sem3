namespace sem3.Models
{
	public class Cart
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int ServiceId { get; set; }
		public int UserId { get; set; }
		public float Price { get; set; }
		public int Quantity { get; set; }
		public float TotalPrice { get; set; }
		public Product Product { get; set; }
		public Service Service { get; set; }
		public User User { get; set; }
	}
}
