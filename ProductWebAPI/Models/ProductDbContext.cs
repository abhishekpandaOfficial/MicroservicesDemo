using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProductWebAPI.Models
{
    public class ProductDbContext:DbContext
    {
        public DbSet<Products> Products { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> dbcontextOptions):base(dbcontextOptions)
        {
			try
			{
                var databaseCreate = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

                if(databaseCreate != null)
                {
                    // create Database if canot connect
                    if(!databaseCreate.CanConnect()) databaseCreate.Create();

                    // Create Table
                    if (!databaseCreate.HasTables()) databaseCreate.CreateTables();
                }


			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}
        }
    }
}
