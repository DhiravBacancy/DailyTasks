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
            

            // Query 1: Join Customers and Orders
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


            // Query 2: GroupJoin Customers and Orders
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


            // Query 3: SelectMany - Cartesian Product of Customers and Orders
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


            // Query 4: Count Orders per Customer
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


            //Query - 5
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
                    OrderAmounts = CustomOrders.Select(o => o.TotalAmount).ToList()
                }).ToList();
            Console.WriteLine("Query 5 (Method Syntax) Output:");
            PrintQueryResults(query5MethodSyntax);
            Console.WriteLine("Query 5 (Query Syntax) Output:");
            PrintQueryResults(query5QuerySyntax);


            //Query - 6
            var query6MethodSyntax = ordersList.ToLookup(o => o.CustomerId);
            var query6QuerySyntax =(from o in ordersList select o).ToLookup(o => o.CustomerId);
            Console.WriteLine("Query 6 (Method Syntax) Output:");
            PrintQueryResults(query6MethodSyntax);
            Console.WriteLine("Query 6 (Query Syntax) Output:");
            PrintQueryResults(query6QuerySyntax);


            //Query - 7
            var query7MethodSyntax = customerList
                .GroupJoin(ordersList,
                    c => c.CustomerId,
                    o => o.CustomerId,
                    (c, customOrders) => new
                    {
                        CustomerId = c.CustomerId,
                        NoOfOrders = customOrders.Count(),
                        HighestOrderTotalAmount = customOrders.Any() ? customOrders.Max(co => co.TotalAmount) : 0 // Avoid exception
                    }).ToList();
                var query7QuerySyntax =
                (from c in customerList
                join o in ordersList on c.CustomerId equals o.CustomerId into customOrders
                select new
                {
                    CustomerId = c.CustomerId,
                    NoOfOrders = customOrders.Count(),
                    HighestOrderTotalAmount = customOrders.Any() ? customOrders.Max(co => co.TotalAmount) : 0
                }
                ).ToList();
            Console.WriteLine("Query 7 (Method Syntax) Output:");
            PrintQueryResults(query7MethodSyntax);
            Console.WriteLine("Query 7 (Query Syntax) Output:");
            PrintQueryResults(query7QuerySyntax);


            //Query - 8
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


            //Query - 9
            var query9MethodSyntax = customerList
                .Select(c => c.City)
                .Distinct()
                .ToList();
            var query9QuerySyntax =
                (from c in customerList
                 select c
                )
                .Distinct()
                .ToList();
            Console.WriteLine("Query 9 (Method Syntax) Output:");
            PrintQueryResults(query9MethodSyntax);
            Console.WriteLine("Query 9 (Query Syntax) Output:");
            PrintQueryResults(query9QuerySyntax);


            //Query - 10
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


            //Query - 11
            var query11MethodSyntax = customerList
                .Select(c => c.CustomerId)
                .Intersect(ordersList.Select(o => o.CustomerId))
                .ToList();
            var query11QuerySyntax = (from c in customerList
                join o in ordersList on c.CustomerId equals o.CustomerId
                select new
                {
                    CustomerName = c.Name
                }
                ).ToList();
            Console.WriteLine("Query 11 (Method Syntax) Output:");
            PrintQueryResults(query11MethodSyntax);
            Console.WriteLine("Query 11 (Query Syntax) Output:");
            PrintQueryResults(query11QuerySyntax);


            //Query - 12
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


            //Query - 13
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
        }

        public static void PrintQueryResults<T>(List<T> results)
        {
            Console.WriteLine("---------------------------------------------------");

            foreach (var item in results)
            {
                if (item == null)
                {
                    Console.WriteLine("Null item");
                    continue;
                }

                var type = typeof(T);

                // ✅ Handle primitive types like string, int, double, etc.
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

                        if (value is IEnumerable<object> collection)
                        {
                            Console.Write($"{prop.Name}: ");
                            Console.Write(collection.Any() ? $"[{string.Join(", ", collection.Select(x => x.ToString()))}]" : "No data found.");
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




