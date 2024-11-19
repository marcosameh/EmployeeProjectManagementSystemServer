using Main.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Main.DAL.Context
{
    public class MainDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Project>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Project>()
           .HasIndex(p => new { p.EmployeeId, p.Name })
           .IsUnique()
           .HasFilter("[IsDeleted] = 0");
        }
    }

}
