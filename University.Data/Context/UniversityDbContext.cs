using Microsoft.EntityFrameworkCore;
using University.Data.Context.Mapping;

namespace University.Data.Entities
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UniversityDb;Trusted_Connection=true;TrustServerCertificate=true;");
        }
    }
}
