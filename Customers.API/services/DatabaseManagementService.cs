using Microsoft.EntityFrameworkCore;

namespace Customers.API.services
{
    public static class DatabaseManagementService
    {
        // Getting the scope of our database context
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Takes all of our migrations files and apply them against the database in case they are not implemented
                serviceScope.ServiceProvider.GetService<CustomerDbContext>().Database.Migrate();
            }
        }
    }
}
