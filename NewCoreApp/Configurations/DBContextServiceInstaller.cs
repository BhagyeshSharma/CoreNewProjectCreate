using Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace NewCoreApp.Configurations
{
    public class DBContextServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(_ => new SqlConnection(configuration.GetConnectionString("DBConnection")));

            services.AddDbContext<UserMgMtContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DBConnection"));
            });

        }
    }
}
