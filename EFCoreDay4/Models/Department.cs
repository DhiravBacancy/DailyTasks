using System.ComponentModel.DataAnnotations;

namespace EFCore_Day_5_Full_CRUD.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [MaxLength(50)]
        public string DepartmentName { get; set; }

        // Navigation Property
        public virtual ICollection<Employee> Employees { get; set; }
    }
}