using System;
using System.Threading.Tasks.Dataflow;

namespace LINQ_Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var (customerList, ordersList) = Customer.customerList();

            var (anotherCustomerList, anotherOrdersList) = Customer.customerList();

            //Customer.printCustomers(customerList);
            //Customer.printC ustomers(anotherCustomerList);



            // Query 1:Create a LINQ query that combines customer information
            //         with their orders. Ensure that only customers who have placed orders appear in the result.
            var query1MethodSyntax = customerList
                .Join(ordersList,
                    c => c.CustomerId,
                    o => o.CustomerId,
                    (c, o) => new
                    {
                        CustomerId = c.CustomerId,
                        CustomerName = c.Name,
                        City = c.City,
                        OrderId = o.OrderId,
                        TotalAmount = o.TotalAmount,
                        OrderDate = o.OrderDate
                    }).ToList();
            var query1QuerySyntax =
                (from c in customerList
                 join o in ordersList on c.CustomerId equals o.CustomerId
                 select new
                 {
                     CustomerId = c.CustomerId,
                     CustomerName = c.Name,
                     City = c.City,
                     OrderId = o.OrderId,
                     TotalAmount = o.TotalAmount,
                     OrderDate = o.OrderDate
                 }).ToList();
            Console.WriteLine("Query 1 (Method Syntax) Output:");
            PrintQueryResults(query1MethodSyntax);
            Console.WriteLine("Query 1 (Query Syntax) Output:");
            PrintQueryResults(query1QuerySyntax);


            // Query 2: Create a Grouped Join to show each customer along with all their orders.
            //          If a customer has no orders, their name should still appear.

            var query2MethodSyntax = customerList
                .GroupJoin(ordersList,
                    c => c.CustomerId,
                    o => o.CustomerId,
                    (c, customOrders) => new
                    {
                        CustomerId = c.CustomerId,
                        CustomerName = c.Name,
                        City = c.City,
                        Orders = customOrders
                    }).ToList();
            var query2QuerySyntax =
                (from c in customerList
                 join o in ordersList on c.CustomerId equals o.CustomerId into CustomOrders
                 select new
                 {
                     CustomerId = c.CustomerId,
                     CustomerName = c.Name,
                     City = c.City,
                     Orders = CustomOrders
                 }).ToList();
            Console.WriteLine("Query 2 (Method Syntax) Output:");
            PrintQueryResults(query2MethodSyntax);
            Console.WriteLine("Query 2 (Query Syntax) Output:");
            PrintQueryResults(query2QuerySyntax);


            // Query 3: Write a query that pairs every customer with every order,
            //          regardless of any relationship between them.
            var query3MethodSyntax = customerList.SelectMany(
                c => ordersList,
                (c, o) => new { c.CustomerId, c.Name, c.City, o.OrderId, o.TotalAmount, o.OrderDate }
            ).ToList();
            var query3QuerySyntax =
               (from c in customerList
                from o in ordersList
                select new { c.CustomerId, c.Name, c.City, o.OrderId, o.TotalAmount, o.OrderDate }).ToList();
            Console.WriteLine("Query 3 (Method Syntax) Output:");
            PrintQueryResults(query3MethodSyntax);
            Console.WriteLine("Query 3 (Query Syntax) Output:");
            PrintQueryResults(query3QuerySyntax);


            // Query 4: Modify the previous queries to return all customers, whether they have orders or not
            var query4MethodSyntax = customerList
                .GroupJoin(ordersList,
                    c => c.CustomerId,
                    o => o.CustomerId,
                    (c, customOrders) => new
                    {
                        CustomerName = c.Name,
                        OrderCount = customOrders.Count()
                    }).ToList();
            var query4QuerySyntax =
                (from c in customerList
                 join o in ordersList on c.CustomerId equals o.CustomerId into CustomOrders
                 select new
                 {
                     CustomerName = c.Name,
                     OrderCount = CustomOrders.Count()
                 }).ToList();
            Console.WriteLine("Query 4 (Method Syntax) Output:");
            PrintQueryResults(query4MethodSyntax);
            Console.WriteLine("Query 4 (Query Syntax) Output:");
            PrintQueryResults(query4QuerySyntax);


            //Query 5 - Write a query to categorize orders by the customer.
            //Show each customer along with a list of their total order amounts.
            var query5MethodSyntax = customerList
                .GroupJoin(ordersList,
                    c => c.CustomerId,
                    o => o.CustomerId,
                    (c, customOrders) => new
                    {
                        CustomerName = c.Name,
                        OrderAmounts = customOrders.Select(o => o.TotalAmount).ToList()
                    }).ToList();
            var query5QuerySyntax = (
                from c in customerList
                join o in ordersList on c.CustomerId equals o.CustomerId into CustomOrders
                select new
                {
                    CustomerName = c.Name,
                    OrderAmounts = (from o in CustomOrders select o.TotalAmount).ToList()
                }).ToList();
            Console.WriteLine("Query 5 (Method Syntax) Output:");
            PrintQueryResults(query5MethodSyntax);
            Console.WriteLine("Query 5 (Query Syntax) Output:");
            PrintQueryResults(query5QuerySyntax);


            //Query - 6 Use an alternative approach (ToLookup) to group customers and their orders.
            //          Compare the output with GroupBy.
            var query6MethodSyntax = ordersList.ToLookup(o => o.CustomerId);
            var query6QuerySyntax =(from o in ordersList select o).ToLookup(o => o.CustomerId);
            Console.WriteLine("Query 6 (Method Syntax) Output:");
            PrintQueryResults(query6MethodSyntax);
            Console.WriteLine("Query 6 (Query Syntax) Output:");
            PrintQueryResults(query6QuerySyntax);


            //Query - 7 Modify the grouped query to return customer ID, number of orders placed,
            //          and the highest order amount per customer.
            var query7MethodSyntax = customerList
                .Join(ordersList,
                    c => c.CustomerId,
                    o => o.CustomerId,
                    (c, o) => new { c.CustomerId, c.Name, o.OrderId, o.TotalAmount })
                .GroupBy(co => new { co.CustomerId, co.Name })
                .Select(g => new
                {
                    g.Key.CustomerId,
                    NoOfOrders = g.Count(),
                    HighestOrderTotalAmount = g.Max(x => x.TotalAmount)
                }).ToList();

            var query7QuerySyntax =
                (from c in customerList
                join o in ordersList on c.CustomerId equals o.CustomerId 
                group o by new { c.CustomerId, c.Name } into groupedOrders
                select new
                {
                    CustomerId = groupedOrders.Key.CustomerId,
                    NoOfOrders = groupedOrders.Count(),
                    HighestOrderTotalAmount = groupedOrders.Max(o => o.TotalAmount)
                }
                ).ToList();
            Console.WriteLine("Query 7 (Method Syntax) Output:");
            PrintQueryResults(query7MethodSyntax);
            Console.WriteLine("Query 7 (Query Syntax) Output:");
            PrintQueryResults(query7QuerySyntax);


            //Query - 8 Find out which customers have placed an order of at least $1000 using a nested LINQ query.
            var query8MethodSyntax = customerList
                .Where(c => ordersList.Any(o => o.CustomerId == c.CustomerId && o.TotalAmount >= 1000))
                .ToList();
            var query8QuerySyntax =
                (from c in customerList
                 where ordersList.Any(o => o.CustomerId == c.CustomerId && o.TotalAmount >= 1000)
                 select c
                ).ToList();
            Console.WriteLine("Query 8 (Method Syntax) Output:");
            PrintQueryResults(query8MethodSyntax);
            Console.WriteLine("Query 8 (Query Syntax) Output:");
            PrintQueryResults(query8QuerySyntax);


            //Query - 9 List all unique cities where customers are located.
            var query9MethodSyntax = customerList
                .Select(c => c.City)
                .Distinct()
                .ToList();
            var query9QuerySyntax =
                (from c in customerList
                 select c.City
                ).Distinct()
                .ToList();
            Console.WriteLine("Query 9 (Method Syntax) Output:");
            PrintQueryResults(query9MethodSyntax);
            Console.WriteLine("Query 9 (Query Syntax) Output:");
            PrintQueryResults(query9QuerySyntax);


            //Query - 10 Find customers present in the first dataset but not in the second.
            var query10MethodSyntax = customerList
                .Select(c => c.Name)
                .Except(anotherCustomerList.Select(c => c.Name))
                .ToList();
            var query10QuerySyntax = (from c in customerList
                                      select c.Name)
                           .Except(from c in anotherCustomerList
                                   select c.Name)
                           .ToList();
            Console.WriteLine("Query 10 (Method Syntax) Output:");
            PrintQueryResults(query10MethodSyntax);
            Console.WriteLine("Query 10 (Query Syntax) Output:");
            PrintQueryResults(query10QuerySyntax);


            //Query - 11 Identify the customers who appear in both datasets.
            var query11MethodSyntax = customerList
                .Select(c => c.Name)
                .Intersect(anotherCustomerList.Select(a => a.Name))
                .ToList();
            var query11QuerySyntax = (from c in customerList
                join a in anotherCustomerList on c.CustomerId equals a.CustomerId
                select new
                {
                    CustomerName = c.Name
                }
                ).ToList();
            Console.WriteLine("Query 11 (Method Syntax) Output:");
            PrintQueryResults(query11MethodSyntax);
            Console.WriteLine("Query 11 (Query Syntax) Output:");
            PrintQueryResults(query11QuerySyntax);


            //Query - 12 Combine two lists of customers from different datasets while ensuring there are no duplicate names.
            var query12MethodSyntax = customerList
                .Select(c => c.Name)
                .Union(anotherCustomerList.Select(c => c.Name))
                .ToList();
            var query12QuerySyntax = (from c in customerList select c.Name)
                .Union(from c in anotherCustomerList select c.Name)
                .ToList();
            Console.WriteLine("Query 12 (Method Syntax) Output:");
            PrintQueryResults(query12MethodSyntax);
            Console.WriteLine("Query 12 (Query Syntax) Output:");
            PrintQueryResults(query12QuerySyntax);


            //Query - 13 Given a collection of customer names that may contain duplicates,
            //           write a LINQ query to remove duplicate entries.
            var query13MethodSyntax = customerList
                .Select(c => c.Name)
                .Distinct()
                .ToList();
            var query13QuerySyntax = (from c in customerList select c.Name)
                .Distinct()
                .ToList();
            Console.WriteLine("Query 13 (Method Syntax) Output:");
            PrintQueryResults(query13MethodSyntax);
            Console.WriteLine("Query 13 (Query Syntax) Output:");
            PrintQueryResults(query13QuerySyntax);


            //Query - 14
            // Eager Loading (Immediate Execution)
            var eagerQuery = customerList.Where(c => c.CustomerId > 1).ToList(); // Query executes immediately

            customerList.Add(new Customer( 11,"Emma", "New York" )); // Modify source list

            Console.WriteLine("\nEager Loading Output:");
            foreach (var customer in eagerQuery) // Data remains unchanged
            {
                Console.WriteLine($"{customer.CustomerId} - {customer.Name}");
            }



            // Lazy loading (query not executed yet)
            var lazyQuery = customerList.Where(c => c.City == "New York");

            // Adding a new customer after defining the query
            customerList.Add(new Customer(21, "Emma", "New York"));

            Console.WriteLine("\nLazy Loading Output:");
            foreach (var customer in lazyQuery) // Query executes here
            {
                Console.WriteLine(customer);
            }
        }

        public static void PrintQueryResults<T>(List<T> results)
        {
            Console.WriteLine("---------------------------------------------------");

            if (results == null || !results.Any())
            {
                Console.WriteLine("No data found.");
                Console.WriteLine("---------------------------------------------------\n");
                return;
            }

            foreach (var item in results)
            {
                if (item == null)
                {
                    Console.WriteLine("Null item");
                    continue;
                }

                var type = typeof(T);

                // ✅ Handle primitive types directly
                if (type.IsPrimitive || type == typeof(string))
                {
                    Console.WriteLine($"  Value: {item}");
                    continue;
                }

                var properties = type.GetProperties()
                                     .Where(p => p.GetIndexParameters().Length == 0) // Exclude indexers
                                     .ToArray();

                foreach (var prop in properties)
                {
                    try
                    {
                        var value = prop.GetValue(item);

                        // ✅ Handle collections, ensuring compatibility with List<int>, List<decimal>, etc.
                        if (value is System.Collections.IEnumerable collection && !(value is string))
                        {
                            var list = collection.Cast<object>().ToList();
                            Console.Write($"{prop.Name}: ");
                            Console.Write(list.Any() ? $"[{string.Join(", ", list)}]" : "No data found.");
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write($"{prop.Name}: {value}  ");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write($"{prop.Name}: [Error: {ex.Message}]  ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("---------------------------------------------------\n");
        }

        public static void PrintQueryResults<TKey, TElement>(ILookup<TKey, TElement> lookup)
        {
            Console.WriteLine("---------------------------------------------------");

            foreach (var group in lookup)
            {
                Console.WriteLine($"Customer ID: {group.Key}");

                foreach (var item in group)
                {
                    if (item == null)
                    {
                        Console.WriteLine("  Null item");
                        continue;
                    }

                    var type = typeof(TElement);
                    if (type.IsPrimitive || type == typeof(string))
                    {
                        Console.WriteLine($"  Value: {item}");
                        continue;
                    }

                    var properties = type.GetProperties()
                                         .Where(p => p.GetIndexParameters().Length == 0) // Exclude indexers
                                         .ToArray();

                    foreach (var prop in properties)
                    {
                        try
                        {
                            var value = prop.GetValue(item);
                            Console.Write($"{prop.Name}: {value}  ");
                        }
                        catch (Exception ex)
                        {
                            Console.Write($"{prop.Name}: [Error: {ex.Message}]  ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------------\n");
        }

    }
}




