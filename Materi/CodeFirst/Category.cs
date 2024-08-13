using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeFirst;

public class Category
{
	
	public int CategoryId { get; set; }
	
	
	public string CategoryName { get; set; }
	
	public string Description { get; set; }
	public IEnumerable<Product> Products { get; set; }
	public Category() 
	{
		Products = new HashSet<Product>();
	}
}