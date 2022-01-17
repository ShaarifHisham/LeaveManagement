using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveTrack.Model
{
    /// <summary>
    /// Leave Request
    /// </summary>
    public class EmployeeLeaveRequest
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        [Required]
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Approver Id
        /// </summary>
        [Required]
        public int ApproverId { get; set; }

        /// <summary>
        /// From Date
        /// </summary>
        [Required]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// To Date
        /// </summary>
        [Required]
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Reason
        /// </summary>
        public string Reason { get; set; }
    }
}
