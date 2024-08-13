using Microsoft.EntityFrameworkCore;

namespace CodeFirst;

public class DataContext : DbContext
{
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Product> Products { get; set; }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=apapun;Username=postgres;Password=postgres");
	}
}
