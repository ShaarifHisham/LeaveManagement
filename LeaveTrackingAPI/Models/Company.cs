using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveTrack.Models
{
    public class Company
    {
        public Company()
        {
            Projects = new HashSet<Project>();
            Employees = new HashSet<Employee>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
