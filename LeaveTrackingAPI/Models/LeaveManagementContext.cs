using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public DbSet<Authentication> Authentications { get; set; }
        public DbSet<LeaveReport> LeaveReports { get; set; }
        public DbSet<LeaveApproval> LeaveApproval { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=LeaveManagementDb;Trusted_Connection=True;");
            }
        }
    }
}
