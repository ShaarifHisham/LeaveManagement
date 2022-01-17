using Microsoft.EntityFrameworkCore;

namespace LeaveTrack.Models
{
    public class LeaveManagementContext : DbContext
    {
        public LeaveManagementContext(DbContextOptions<LeaveManagementContext> options) : base(options)
        {
        }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<LeaveRequest> LeaveReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=LeaveManagementDb;Trusted_Connection=True;");
            }
        }
    }
}
