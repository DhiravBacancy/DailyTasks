using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;

namespace LINQ_Day2
{
    class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<Order> Orders { get; set; } // Added Order list inside Customer

        // Static lists to store customers and orders
        public static List<Customer> customers = new List<Customer>();
        public static List<Order> orders = new List<Order>();

        public Customer(int id, string name, string city)
        {
            CustomerId = id;
            Name = name;
            City = city;
            Orders = new List<Order>(); // Initialize the Orders list
        }

        public static (List<Customer>, List<Order>) customerList()
        {
            Random rand = new Random();
            string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix" };
            List<string> names = new List<string>
    {
        "Alice", "Bob", "Charlie", "David", "Emily", "Frank", "Grace", "Henry",
        "Isabella", "Jack", "Kevin", "Laura", "Michael", "Natalie", "Oliver",
        "Peter", "Quinn", "Rachel", "Sophia", "Thomas"
    };

            // Shuffle names to ensure a random order
            names = names.OrderBy(_ => rand.Next()).ToList();

            List<Customer> customers = new List<Customer>();
            List<Order> orders = new List<Order>();

            try
            {
                for (int i = 1; i <= 20; i++)
                {
                    string city = cities[rand.Next(cities.Length)];
                    string name;

                    // Ensure unique names where possible
                    if (i <= names.Count)
                        name = names[i - 1]; // Pick unique name from shuffled list
                    else
                        name = names[rand.Next(names.Count)]; // Allow some duplicates after unique names are used

                    Customer customer = new Customer(i, name, city);
                    customers.Add(customer);

                    // Get orders for this customer and add them to both lists
                    List<Order> customerOrders = Order.orderList(i);
                    customer.Orders.AddRange(customerOrders);
                    orders.AddRange(customerOrders);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating customers: {ex.Message}");
            }

            return (customers, orders);
        }


        public static void printCustomers()
        {
            try
            {
                foreach (var customer in customers)
                {
                    Console.WriteLine($"\n{customer}");
                    if (customer.Orders.Count > 0)
                    {
                        Console.WriteLine("  Orders:");
                        foreach (var order in customer.Orders)
                        {
                            Console.WriteLine($"    {order}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No orders found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error printing customers: {ex.Message}");
            }
        }

        public override string ToString()
        {
            return $"ID: {CustomerId}, Name: {Name}, City: {City}";
        }
    }

    class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Static list to store orders
        public static List<Order> orders = new List<Order>();

        public Order(int orderId, int customerId, decimal amount, DateTime orderDate)
        {
            OrderId = orderId;
            CustomerId = customerId;
            TotalAmount = amount;
            OrderDate = orderDate;
        }

        private static Random rand = new Random();

        public static List<Order> orderList(int customerId)
        {
            List<Order> customerOrders = new List<Order>(); // Local list to store new orders
            try
            {
                int orderCount = rand.Next(0, 5);
                for (int i = 0; i < orderCount; i++)
                {
                    Order order = new Order(
                        rand.Next(1000, 9999), // Order ID
                        customerId,
                        rand.Next(50, 5000), // Amount
                        DateTime.Now.AddDays(-rand.Next(1, 30))
                    );
                    customerOrders.Add(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating orders: {ex.Message}");
            }
            return customerOrders; // Return only the newly generated orders
        }

        public override string ToString()
        {
            return $"OrderID: {OrderId}, CustomerID: {CustomerId}, Amount: {TotalAmount}, Date: {OrderDate.ToShortDateString()}";
        }
    }
}
