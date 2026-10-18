using LINQ.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO.Pipelines;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using static LINQ.DataSources.Source;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
            // 1.Get top 3 most expensive products 

            //var result = ProductList.
            //    OrderByDescending(p => p.UnitPrice).Take(3);
            //print (result);
            // 2.show page 2 of products, with page size = 5 

            //var result = ProductList.Skip((2 - 1) * 5).Take(5);
            //print(result);

            // 3.Take products from the list as long as Their UnitPrice is less than $25(list is ordered by price).
            //var result = ProductList.OrderBy(p => p.UnitPrice).TakeWhile(p => p.UnitPrice < 25);
            //print(result);


            // 4.Check if ALL products in the "Seafood" category are in stock 
            //var result = ProductList.Where(p => p.Category == "Seafood").All(p => p.UnitsInStock > 0);

            // 5.Check if the ID list contains 9
            //int[] ids = { 3, 9, 13, 18 };

            //var result = ids.Contains(9);
            //Console.WriteLine(result);

            //6.Group all products by Category and print each group  with its product count.

            //var result = ProductList.GroupBy(p=>p.Category);

            //foreach (var item in result) 
            //{
            //    Console.WriteLine($"{item.Key} -- Count : {item.Count()}");
            //}


            //7.Group products by Category and project only product names  per group
            //var result = ProductList.GroupBy(p => p.Category);
            //foreach (var item in result) 
            //{
            //    foreach(var item2 in item) 
            //    {
            //        Console.WriteLine(item2.ProductName); 

            //    }
            //    Console.WriteLine("===================");
            //}

            //8.Find all categories that have MORE THAN 3 products 

            //var result = ProductList.GroupBy(p => p.Category)
            //    .Where(g=>g.Count()>3);

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{item.Key} -- Count : {item.Count()}");
            //}

            //9.Using QUERY SYNTAX,
            //group customers by Country, and for  each group
            //select { Country, Count, TotalOrderValue }.
            

            //10.Calculate the total number of units in stock across all products 

            //var result = ProductList.Sum(p => p.UnitsInStock);

            ////11.Find the CHEAPEST and MOST EXPENSIVE product prices
            //var result2 = ProductList.Min(p => p.UnitPrice);
            //var result3 = ProductList.Max(p => p.UnitPrice);

            //12.Get a distinct list of all product categories
            
            var result = ProductList.Select(p => p.Category).Distinct();


            //13.find product IDs that are in setA but NOT in 
            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };

            var result2 = setA.Except(setB);


            //14.Find countries that  appear in list1 but NOT in list2(case -insensitive).
            //string[] list1 = { "Germany", "France", "UK", "Spain" };
            //string[] list2 = { "france", "SPAIN", "Italy" };
            //var result3 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);

            ////15.Build a Dictionary<int, Product> keyed by ProductID. Then retrieve and print the product with ID = 18.
            //var productDictionary = ProductList.ToDictionary(p => p.ProductID);
            //Console.WriteLine(productDictionary[18]);

            //16.Get the first product whose price is greater than $50.
            var result4 = ProductList.FirstOrDefault(p => p.UnitPrice > 50);
            Console.WriteLine(result4);
            //17.Try to get the first product with a price > $500.it returns null instead of throwing.
            var result5 = ProductList.FirstOrDefault(p => p.UnitPrice > 500);

            //18.Generate a multiplication table row for 7
            var result6 = Enumerable.Range(0, 13).Select(i => $"7 x {i} = {7 * i}");
            print(result6);
            //19.Generate even numbers between 1 and 30.
            var result7 = Enumerable.Range(1, 30).Where(i=>i%2==0);
            print(result7);
            //20.Concatenate the first 3 product names with the first  3 customer company names into a single sequence.
            var result8 = ProductList.Select(p => p.ProductName).Take(3)
                .Concat(CustomerList.Select(c => c.CustomerName).Take(3));

            //21.Pair each product with a customer(by position)  and produce a string "ProductName sold to CompanyName".
            var result9= ProductList.Select(p => p.ProductName)
            .Zip(CustomerList.Select(c => c.CustomerName), (p, c) => $"{p} sold to {c}");
            print(result9);
        }
        }
}
