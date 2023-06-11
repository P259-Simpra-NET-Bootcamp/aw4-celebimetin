using Core.Configurations;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Extensions
{
    public static class CustomDbContextExtension
    {
        public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            var dbType = Configuration.GetConnectionString(AppSettings.DbType);
            if (dbType == AppSettings.Mssql)
            {
                var dbConfig = Configuration.GetConnectionString(AppSettings.MsSqlConnection);
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConfig));
            }
            else if (dbType == AppSettings.PostgreSql)
            {
                var dbConfig = Configuration.GetConnectionString(AppSettings.PostgreSqlConnection);
                services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConfig));
            }

            services.AddScoped<DapperDbContext>();
        }
    }
}