using Microsoft.EntityFrameworkCore;
using MyMvc.Models;

namespace MyMvc.Databases;

public class DataContex:DbContext
{
    public DataContex(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
}
