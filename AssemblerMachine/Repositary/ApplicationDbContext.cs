
using AssemblerMachine.DataAccess;
using AssemblerMachine.Model;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext 
{
    public DbSet<Component> Components { get; set; }
    public DbSet<Car> Cars { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseSqlServer("Data Source =.; Initial Catalog =CarFactorySystem; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=true;");
	// "Server=192.168.1.2;Database=master;User Id=sa;Password=abc@123;Encrypt=True;TrustServerCertificate=True"
}
