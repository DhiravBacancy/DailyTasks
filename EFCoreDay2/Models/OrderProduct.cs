﻿using EFCoreDay2.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderProduct
{
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
}
