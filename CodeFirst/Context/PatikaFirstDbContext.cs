using CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Context
{
    public class PatikaFirstDbContext : DbContext
    {
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<GameEntity> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=PatikaCodeFirstDb1;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
