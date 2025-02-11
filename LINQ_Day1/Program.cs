using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Day1
{
    class Program
    {
        public static void Main()
        {
            List<Customer> staticList = Customer.customerList();
            Customer.printCustomers(staticList);
            printSection();


            //Query - 1
            //Method Syntax
            var query1MethodSyntax = staticList.Where(c => c.City == "New York").ToList();
            Console.WriteLine("Query 1 Method Syntax:");
            printAnswer(query1MethodSyntax);
            //Query Syntax
            var query1QuerySyntax = from c in staticList where c.City == "New York" select c;
            Console.WriteLine("Query 1 Query Syntax:");
            printAnswer(query1QuerySyntax.ToList());
            printSection();


            ////Query - 2
            //Method Syntax 
            var query2MethodSyntax = staticList.Where(c => c.Age > 25).OrderBy(c => c.Age).ToList();
            Console.WriteLine("Query 2 Method Syntax:");
            printAnswer(query2MethodSyntax);
            //Query Syntax
            var query2QuerySyntax = from c in staticList where c.Age > 25 orderby c.Age ascending select c;
            Console.WriteLine("Query 2 Query Syntax:");
            printAnswer(query2QuerySyntax.ToList());
            printSection();


            //Query - 3
            //Method Syntax 
            int customerId = 10;
            var query3MethodSyntax = staticList.Where(c => c.Id == customerId)
                                               .SelectMany(c => c.OrderList)
                                               .OrderByDescending(o => o.OrderDate)
                                               .ToList();
            Console.WriteLine("\nQuery 3 Method Syntax:");
            printAnswer(query3MethodSyntax);
            //Query Syntax
            var query3QuerySyntax = from c in staticList
                                    where c.Id == customerId
                                    from o in c.OrderList
                                    orderby o.OrderDate descending
                                    select o;
            Console.WriteLine("\nQuery 3 Query Syntax:");
            printAnswer(query3QuerySyntax.ToList());
            printSection();


            //Query -4
            //Method Syntax 
            var query4MethodSyntax = staticList.Select(c => new { c.Name, c.City })
                                               .ToList();
            Console.WriteLine("\nQuery 4 Method Syntax:");
            printAnswer(query4MethodSyntax);
            //Query Syntax
            var query4QuerySyntax = from c in staticList
                                    select new { c.Name, c.City };
            Console.WriteLine("\nQuery 4 Query Syntax:");
            printAnswer(query4QuerySyntax.ToList());
            printSection();


            //Query - 5
            //Method Syntax
            var query5MethodSyntax = staticList.Select(c => new { c, c.OrderList.Count }).ToList();
            Console.WriteLine("\nQuery 5 Method Syntax:");
            printAnswer(query5MethodSyntax);
            //Query Syntax
            var query5QuerySyntax = from c in staticList
                                    select new { c, c.OrderList.Count };
            Console.WriteLine("\nQuery 5 Query Syntax:");
            printAnswer(query5QuerySyntax.ToList());
            printSection();


            //Query 6 
            //Method Syntax
            var query6MethodSyntax = staticList.SelectMany(c => c.OrderList).ToList();
            Console.WriteLine("\nQuery 6 Method Syntax:");
            printAnswer(query6MethodSyntax);
            ////Query Syntax
            var query6QuerySyntax = (from c in staticList
                                     from o in c.OrderList
                                     select o).ToList();
            Console.WriteLine("\nQuery 6 Query Syntax:");
            printAnswer(query6MethodSyntax);
            printSection();


            //Query - 7
            //Method Syntax
            int totalCustomers = staticList.Count();
            double averageAge = staticList.Average(c => c.Age);
            Console.WriteLine("\nQuery 7 Method Syntax: Total Customers - " + totalCustomers +" Average Age - " + averageAge);
            ////Query Syntax
            averageAge = (from c in staticList
                          select c.Age).Average();
            totalCustomers = staticList.Count();
            Console.WriteLine("\nQuery 7 Query Syntax: Total Customers - " + totalCustomers + " Average Age - " + averageAge);
            printSection();


            //Query - 8
            //Method Syntax
            double minOrder = staticList.SelectMany(c => c.OrderList).Min(o => o.Amount);
            double maxOrder = staticList.SelectMany(c => c.OrderList).Max(o => o.Amount);
            Console.WriteLine("\nQuery 8 Method Syntax: Min Order Amount - " + minOrder + " Max Order Amount - " + maxOrder);
            ////Query Syntax
            minOrder = (from c in staticList
                        from o in c.OrderList
                        select o.Amount).Min();

            maxOrder = (from c in staticList
                        from o in c.OrderList
                        select o.Amount).Max();
            Console.WriteLine("\nQuery 8 Query Syntax: Min Order Amount - " + minOrder + " Max Order Amount - " + maxOrder);
            printSection();


            //Query - 9
            //Method Syntax
            var groupedByCity = staticList
                                .GroupBy(c => c.City)
                                .Select(g => new { City = g.Key, Customers = g.Select(c => c.Name) });
            Console.WriteLine("\nQuery 9 Method Syntax:");
            foreach (var group in groupedByCity)
            {
                Console.WriteLine($"City: {group.City}");
                Console.WriteLine("Customers: " + string.Join(", ", group.Customers));
                Console.WriteLine();
            }
            //Query Syntax
            var groupedByCity2 = from c in staticList
                                 group c.Name by c.City;
            Console.WriteLine("\nQuery 9 Query Syntax:");
            foreach (var group in groupedByCity2)
            {
                Console.WriteLine($"City: {group.Key}");
                Console.WriteLine("Customers: " + string.Join(", ", group));
                Console.WriteLine();
            }
            printSection();


            //Query - 10
            //Method Syntax
            var totalOrderAmountByCustomer = staticList
                                            .GroupBy(c => c.Name)
                                            .Select(g => new { CustomerName = g.Key, TotalAmount = g.Sum(c => c.OrderList.Sum(o => o.Amount)) });
            Console.WriteLine("\nQuery 10 Method Syntax:");
            foreach (var customer in totalOrderAmountByCustomer)
            {
                Console.WriteLine($"Customer: {customer.CustomerName}, Total Order Amount: {customer.TotalAmount}");
            }
            //Query Syntax
            var totalOrderAmountByCustomer2 =from c in staticList
                                             group c by c.Name into customerGroup
                                             select new
                                             {
                                                 CustomerName = customerGroup.Key,
                                                 TotalAmount = customerGroup.Sum(c => c.OrderList.Sum(o => o.Amount))
                                             };
            Console.WriteLine("\nQuery 10 Query Syntax:");
            foreach (var customer in totalOrderAmountByCustomer2)
            {
                Console.WriteLine($"Customer: {customer.CustomerName}, Total Order Amount: {customer.TotalAmount}");
            }


        }

        public static void printAnswer<T>(List<T> obj)
        {
            foreach (var item in obj)
            {
                Console.WriteLine(item);
            }
        }
        public static void printSection()
        {
            Console.WriteLine();
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine();
        }
    }
}
