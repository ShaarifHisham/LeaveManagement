using System;

namespace LeaveTrack.Model
{
    /// <summary>
    /// Leave Report
    /// </summary>
    public class LeaveReportResponse
    {
        /// <summary>
        /// Employee Id
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// From Date
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// To Date
        /// </summary>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Reason
        /// </summary>
        public string Reason { get; set; }
    }
}
