using LeaveTrack.Enums;
using System.ComponentModel.DataAnnotations;

namespace LeaveTrack.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public RoleType EnumValue { get; set; }

        public bool IsDeleted { get; set; }
    }
}
