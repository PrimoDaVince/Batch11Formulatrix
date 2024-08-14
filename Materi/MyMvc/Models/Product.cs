namespace MyMvc.Models;

public class Product
{
	public int ProductId { get; set; }
	public string ProductName { get; set; }
	public string Description { get; set; }
	public Category category{ get; set; }
}
