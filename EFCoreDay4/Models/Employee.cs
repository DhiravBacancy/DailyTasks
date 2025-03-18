using System.ComponentModel.DataAnnotations;

namespace EFCore_Day_5_Full_CRUD.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }

        // Navigation Property
        public virtual Department Department { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}