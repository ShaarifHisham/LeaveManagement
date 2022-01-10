using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveTrack.Models
{
    public class LeaveApproval
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ApproverId { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
        public int? LeaveReportId { get; set; }
        [ForeignKey("LeaveReportId")]
        public virtual LeaveReport LeaveReport { get; set; }
    }
}
