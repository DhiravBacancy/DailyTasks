using System;
using System.Collections.Generic;

namespace LINQ_Day1
{
    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public List<Order> OrderList { get; set; } = new List<Order>(); // Ensure it’s never null

        public Customer(int id, string name, int? age, string city, List<Order> orders)
        {
            Id = id;
            Name = name;
            Age = age ?? 0;
            City = city;
            OrderList = orders ?? new List<Order>();
        }

        public static List<Customer> customerList()
        {
            List<Customer> customers = new List<Customer>();
            Random rand = new Random();
            string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix" };

            try
            {
                for (int i = 1; i <= 20; i++)
                {
                    string city = (i == 1) ? "New York" : cities[rand.Next(cities.Length)];
                    int age = rand.Next(18, 60);
                    List<Order> orders = Order.orderList(rand.Next(1, 4) + 1);

                    customers.Add(new Customer(i, $"Customer {i}", age, city, orders));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating customers: {ex.Message}");
            }

            return customers;
        }

        public static void printCustomers(List<Customer> staticList)
        {
            try
            {
                foreach (var customer in staticList)
                {
                    Console.WriteLine("\n" + customer);
                    Console.WriteLine("Orders:");

                    foreach (var order in customer.OrderList)
                    {
                        Console.WriteLine("  " + order);
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
            return $"ID: {Id}, Name: {Name}, Age: {Age}, City: {City}, Orders: {OrderList.Count}";
        }
    }

    class Order
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(int orderId, double amount, DateTime orderDate)
        {
            OrderId = orderId;
            Amount = amount;
            OrderDate = orderDate;
        }

        private static Random rand = new Random();

        public static List<Order> orderList(int count)
        {
            List<Order> orders = new List<Order>();

            try
            {
                for (int i = 1; i <= count; i++)
                {
                    orders.Add(new Order(
                        rand.Next(1000, 9999),
                        rand.Next(50, 5000),  // ✅ Amount will now be assigned properly
                        DateTime.Now.AddDays(-rand.Next(1, 30))
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating orders: {ex.Message}");
            }

            return orders;
        }

        public override string ToString()
        {
            return $"OrderID: {OrderId}, Amount: {Amount}, Date: {OrderDate.ToShortDateString()}";
        }
    }
}
