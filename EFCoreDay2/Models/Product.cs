using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDay2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Decimal precision for price
        public decimal Price { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
