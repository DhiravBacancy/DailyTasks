using System.ComponentModel.DataAnnotations;

namespace EFCore_Day_5_Full_CRUD.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [MaxLength(50)]
        public string ProjectName { get; set; }
        public DateOnly StartDate { get; set; }

        // Navigation Property
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}