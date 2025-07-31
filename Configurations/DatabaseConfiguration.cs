using DevConnect.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
    }
}
