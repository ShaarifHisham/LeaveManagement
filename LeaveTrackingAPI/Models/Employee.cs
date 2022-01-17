using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveTrack.Models
{
    public class Employee : ISoftDeletable
    {
        public Employee()
        {
            Projects = new HashSet<Project>();
        }
        [Key]
        public int EmployeeId { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
       
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
