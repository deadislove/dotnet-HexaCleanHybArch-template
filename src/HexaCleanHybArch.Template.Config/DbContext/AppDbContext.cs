using HexaCleanHybArch.Template.Config.Loader;
using Microsoft.EntityFrameworkCore;
using MSDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace HexaCleanHybArch.Template.Config.DbContext
{
    public class AppDbContext: MSDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AdapterEntityLoader.ApplyAdapterEntityConfiguration(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
