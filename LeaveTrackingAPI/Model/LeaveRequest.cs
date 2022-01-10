using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveTrack.Model
{
    public class LeaveRequest
    {
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public int ApproverId { get; set; }
        public string Reason { get; set; }

        [Required(ErrorMessage = "Employee Id is required")]
        public int? EmployeeId { get; set; }
    }
}
