using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Repositories.Relational
{
    public class PrincipalDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PrincipalDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration();

            base.OnModelCreating(modelBuilder);
        }
    }
}
