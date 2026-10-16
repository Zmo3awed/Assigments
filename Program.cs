using LINQ.Models;
using System.Security.Cryptography;
using static LINQ.DataSources.Source;
namespace LINQ
{
    internal class Program
    {
        public static void print<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
        static void Main(string[] args)
        {
            // 1. Get all products from the "Seafood" category. Print each
            //product's name and price.

            //var result = ProductList.Where(p => p.Category == "Seafood");
            //print(result);

            //2. Get a list of only the product names from ProductList. Print
            //each name.

            //var result = ProductList.Select(p => p.ProductName);
            //print(result);

            //3. Sort all products by UnitPrice (ascending). Print each
            //product's name and price.

            //var result = ProductList.Select(p => new {productName = p.ProductName ,productPrice = p.UnitPrice})
            //     .OrderByDescending(p=>p.productPrice);
            //print(result);

            //4. Get all products where UnitPrice is between 10 and 30

            //var result = ProductList.Where(p => p.UnitPrice >= 10 && p.UnitPrice <= 30);
            //print(result);



            //5. Get all products that are in stock (UnitsInStock > 0) and
            //belong to the "Condiments" category.

            //var result = ProductList.Where(p => p.UnitsInStock > 0 && p.Category == "Condiments");
            //print(result);


            //6. Create a new anonymous type with three properties:
            //● Name → the product name
            //● Price → the unit price
            //● StockStatus → a string: "Available" if UnitsInStock > 0,
            //otherwise "Out of Stock"
            //● Print the result.

            //var result = ProductList
            //    .Select(p => new { Name = p.ProductName, Price = p.UnitPrice, Stock = p.UnitsInStock > 0 ? "Available" : "Out of Stock" });

            //print(result);



            //7. Print each product's name along with its position (1-based)
            //in the list. Expected format: 1. Chai, 2. Chang, etc.


            //var result = ProductList.Select((p,i)=>$"{i+1} - {p.ProductName}" );
            //print(result);

            //8. Sort ProductList by Category ascending, then within each
            //category, sort by UnitPrice descending.

            //var result = ProductList.OrderBy(p=>p.Category).ThenBy(p=>p.UnitPrice);
            //print(result);


            //9. Get all products from the "Beverages" category, sorted by
            //UnitsInStock descending. Print name and stock.

            //var result = ProductList.Where(p=>p.Category == "Beverages").OrderBy(p=>p.UnitsInStock);
            //print(result);

            //10. Using QUERY SYNTAX with a compound from clause, list
            //all orders placed in 1997 or later showing CustomerID and
            //OrderDate.
            //var result = from c in CustomerList
            //             from or in c.Orders
            //             where or.OrderDate >= new DateTime(1997, 01, 01)
            //             select new { c.CustomerID, or.OrderDate };
            //print(result);






            //11. Show position number alongside ProductName

            //var result=  ProductList.Select((p, i)=> $"{i} -- {p.ProductName}");
            // print(result);

            //12. Sort first by-word length and then by a
            //case-insensitive sort of the words in an array.

            //String[] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            //var result = Arr.OrderBy(p => p.Length)
            //               .ThenBy(p => p, StringComparer.OrdinalIgnoreCase);
            //print(result);

            //13. Create a list of all digits in the array whose second
            //letter is 'i' that is reversed from the order in the
            //original arra
            //String[] Arr = { "Ali", "Ziad", "Hi", "Zinab", "Mohamed", "Siry" };

            //var result = Arr
            //   .Where(p => p.Length > 1 && p[1] == 'i')
            //   .Reverse();

            //print(result);

        }
    }
}
