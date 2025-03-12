namespace EFCoreDay3.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsVIP { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
