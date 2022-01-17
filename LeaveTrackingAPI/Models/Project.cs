using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveTrack.Models
{
    public class Project : ISoftDeletable
    {
        [Key]
        public int ProjectId { get; set; }      
        public string ProjectName { get; set; }
        public DateTime? StartOn { get; set; }
        public DateTime? CompleteOn { get; set; }
        public bool IsDeleted { get; set; }

        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]       
        public virtual Company Company { get; set; }  
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
