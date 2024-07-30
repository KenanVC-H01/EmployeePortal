using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EmployeePortal.Web.Models.Entities;

namespace EmployeePortal.Web.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.salary)
                .HasColumnType("decimal(18,2)");  // SQL Server type with precision and scale
        }
    }
}
