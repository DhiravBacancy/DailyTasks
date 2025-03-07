using System.ComponentModel.DataAnnotations;

namespace EFCoreDay2.Models
{

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}

