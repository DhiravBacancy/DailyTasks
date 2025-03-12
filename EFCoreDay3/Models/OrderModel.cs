namespace EFCoreDay3.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsDeleted { get; set; } = false;


        //Navigation Property
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
