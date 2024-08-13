using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst;

public class Product
{
	public int ProductId { get; set; }

	[MaxLength(40)]
	
	public string ProductName { get; set; }
	
	
	public string Description { get; set; }
	
	public int CategoryId { get; set; }
	public Category Category { get; set; }
	
	[Column(TypeName = "money")]
	public int UnitPrice { get; set; }
	
	[Column(TypeName = "smallint")]
	public int UnitInStock { get; set; }
	
	[Column(TypeName = "smallint")]
	public int UnitOnOrder { get; set; }
	
	[Column(TypeName = "smallint")]
	public int RecorderLevel { get; set; }
	
	


}
