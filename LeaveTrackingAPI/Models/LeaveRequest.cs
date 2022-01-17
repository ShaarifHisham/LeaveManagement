using LeaveTrack.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveTrack.Models
{
    public class LeaveRequest : ISoftDeletable
    {
        [Key]
        public int LeaveReportId { get; set; }     
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ApproverId { get; set; }
        public string Reason { get; set; }
        public virtual EmployeeLeaveStatus Status { get; set; }
        public bool IsDeleted { get; set; }

        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
