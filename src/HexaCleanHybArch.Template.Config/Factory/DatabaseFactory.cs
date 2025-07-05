using HexaCleanHybArch.Template.Config.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HexaCleanHybArch.Template.Config.Factory
{
    public enum DatabaseProvider
    {
        PostgreSQL,
        MSSQL,
        SQLite
    }
    public static class DatabaseFactory
    {
        public static void RegisterDatabase(IServiceCollection services, IConfiguration config)
        {
            DatabaseProvider provider = Enum.Parse<DatabaseProvider>(config["Database:Provider"]!);
            string connectionString = config.GetConnectionString("Default") ?? string.Empty;

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'Default' not found.");

            services.AddDbContext<AppDbContext>(options =>
            {
                switch (provider)
                {
                    case DatabaseProvider.PostgreSQL:
                        options.UseNpgsql(connectionString);
                        break;
                    case DatabaseProvider.MSSQL:
                        options.UseSqlServer(connectionString);
                        break;
                    case DatabaseProvider.SQLite:
                        options.UseSqlite(connectionString);
                        break;
                }
            });
        }
    }
}
