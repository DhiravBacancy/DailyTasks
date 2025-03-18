using System.ComponentModel.DataAnnotations;

namespace EFCore_Day_5_Full_CRUD.Models
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        [MaxLength(20)]
        public string Role { get; set; }

        // Navigation Property
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}