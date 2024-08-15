namespace MyApi.Databases;
using MyApi.Models;	
using Microsoft.EntityFrameworkCore;




public class DataContex:DbContext
{
	public DataContex(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
}
