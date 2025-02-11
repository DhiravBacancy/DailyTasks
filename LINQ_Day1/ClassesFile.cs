using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

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
            OrderList = orders ?? new List<Order>(); // Ensure a valid list is assigned
        }

        public static List<Customer> customerList()
        {
            List<Customer> customers = new List<Customer>();
            Random rand = new Random();

            for (int i = 1; i <= 20; i++)
            {
                List<Order> orders = Order.orderList(rand.Next(1, 4)); // Already ensures a valid list
                customers.Add(new Customer(i, $"Customer {i}", rand.Next(18, 60), $"City {i}", orders));
            }

            return customers;
        }

        public static void printCustomers(List<Customer> staticList)
        {
            foreach (var customer in staticList)
            {
                Console.WriteLine("\n" + customer);
                Console.WriteLine("Orders:");

                foreach (var order in customer.OrderList) // OrderList is now always initialized
                {
                    Console.WriteLine("  " + order);
                }
            }
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Age: {Age}, City: {City}, Orders: {OrderList.Count}";
        }
    }



    class Order
    {
        private int _orderId;
        private double _amount;
        private DateTime _orderDate;

        public int OrderId { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(int orderId, double amount, DateTime orderDate)
        {
            _orderId = orderId;
            _amount = amount;
            _orderDate = orderDate;
        }

        public static List<Order> orderList(int count)
        {
            List<Order> orders = new List<Order>();
            Random rand = new Random();

            for (int i = 1; i <= count; i++)
            {
                orders.Add(new Order(rand.Next(1000, 9999), rand.Next(100, 1000), DateTime.Now.AddDays(-rand.Next(1, 30))));
            }

            return orders;
        }

        public override string ToString()
        {
            return $"OrderID: {_orderId}, Amount: {_amount}, Date: {_orderDate.ToShortDateString()}";
        }
    }

}
