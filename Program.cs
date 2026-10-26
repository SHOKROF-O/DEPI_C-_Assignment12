using static Day_01_G03.ListGenerator;
namespace Day_01_G03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Customers.xml and dictionary_english.txt must be copied to the output folder
            // (Properties -> Copy to Output Directory -> Copy always)

            #region LINQ - Restriction Operators

            #region Q1 Find all products that are out of stock
            var outOfStock = ProductsList.Where(p => p.UnitsInStock == 0);

            foreach (var item in outOfStock)
                Console.WriteLine(item);
            #endregion

            #region Q2 In stock and cost more than 3.00 per unit
            var inStock = ProductsList.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00M);

            foreach (var item in inStock)
                Console.WriteLine(item);
            #endregion

            #region Q3 Digits whose name is shorter than their value
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            // the value of the digit is its index in the array
            var shortNames = digits.Where((d, i) => d.Length < i);

            foreach (var item in shortNames)
                Console.WriteLine(item);
            #endregion

            #endregion

            #region LINQ - Element Operators

            #region Q1 First product out of stock
            var firstOut = ProductsList.First(p => p.UnitsInStock == 0);
            Console.WriteLine(firstOut);
            #endregion

            #region Q2 First product whose price > 1000, or null
            var expensive = ProductsList.FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine(expensive is null ? "null" : expensive.ToString());
            #endregion

            #region Q3 Second number greater than 5
            int[] arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var secondGreater = arr.Where(n => n > 5).ElementAt(1);
            Console.WriteLine(secondGreater);
            #endregion

            #endregion

            #region LINQ - Aggregate Operators

            #region Q1 Count of odd numbers
            Console.WriteLine(arr.Count(n => n % 2 != 0));
            #endregion

            #region Q2 Customers and how many orders each has
            var customersOrders = CustomersList.Select(c => new { c.CustomerName, OrdersCount = c.Orders.Length });

            foreach (var item in customersOrders)
                Console.WriteLine(item);
            #endregion

            #region Q3 Categories and how many products each has
            var categoriesCount = ProductsList.GroupBy(p => p.Category)
                                              .Select(g => new { Category = g.Key, ProductsCount = g.Count() });

            foreach (var item in categoriesCount)
                Console.WriteLine(item);
            #endregion

            #region Q4 Total of the numbers in an array
            Console.WriteLine(arr.Sum());
            #endregion

            string[] words = File.ReadAllLines("dictionary_english.txt");

            #region Q5 Total number of characters of all words
            Console.WriteLine(words.Sum(w => w.Length));
            #endregion

            #region Q6 Length of the shortest word
            Console.WriteLine(words.Min(w => w.Length));
            #endregion

            #region Q7 Length of the longest word
            Console.WriteLine(words.Max(w => w.Length));
            #endregion

            #region Q8 Average length of the words
            Console.WriteLine(words.Average(w => w.Length));
            #endregion

            #endregion

            #region LINQ - Ordering Operators

            #region Q1 Sort products by name
            var byName = ProductsList.OrderBy(p => p.ProductName);

            foreach (var item in byName)
                Console.WriteLine(item);
            #endregion

            #region Q2 Case-insensitive sort using a custom comparer
            string[] fruits = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedFruits = fruits.OrderBy(f => f, new CaseInsensitiveComparer());

            foreach (var item in sortedFruits)
                Console.WriteLine(item);
            #endregion

            #region Q3 Sort products by units in stock (highest to lowest)
            var byStock = ProductsList.OrderByDescending(p => p.UnitsInStock);

            foreach (var item in byStock)
                Console.WriteLine(item);
            #endregion

            #region Q4 Sort digits by length of name, then alphabetically
            var sortedDigits = digits.OrderBy(d => d.Length).ThenBy(d => d);

            foreach (var item in sortedDigits)
                Console.WriteLine(item);
            #endregion

            #region Q5 Sort by word length, then case-insensitive
            var byLengthThenName = fruits.OrderBy(f => f.Length).ThenBy(f => f, new CaseInsensitiveComparer());

            foreach (var item in byLengthThenName)
                Console.WriteLine(item);
            #endregion

            #region Q6 Sort products by category, then by unit price (highest to lowest)
            var byCategoryThenPrice = ProductsList.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);

            foreach (var item in byCategoryThenPrice)
                Console.WriteLine(item);
            #endregion

            #region Q7 Sort by word length, then case-insensitive descending
            var byLengthThenNameDesc = fruits.OrderBy(f => f.Length).ThenByDescending(f => f, new CaseInsensitiveComparer());

            foreach (var item in byLengthThenNameDesc)
                Console.WriteLine(item);
            #endregion

            #region Q8 Digits whose second letter is 'i', reversed
            var secondI = digits.Where(d => d[1] == 'i').Reverse();

            foreach (var item in secondI)
                Console.WriteLine(item);
            #endregion

            #endregion

            #region LINQ - Transformation Operators

            #region Q1 Names of the products
            var productNames = ProductsList.Select(p => p.ProductName);

            foreach (var item in productNames)
                Console.WriteLine(item);
            #endregion

            #region Q2 Uppercase and lowercase versions of each word (Anonymous Type)
            string[] someWords = { "aPPLE", "BlUeBeRrY", "cHeRry" };

            var upperLower = someWords.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });

            foreach (var item in upperLower)
                Console.WriteLine(item);
            #endregion

            #region Q3 Some properties of Products, UnitPrice renamed to Price
            var productsInfo = ProductsList.Select(p => new { p.ProductName, p.Category, Price = p.UnitPrice });

            foreach (var item in productsInfo)
                Console.WriteLine(item);
            #endregion

            #region Q4 Does the value match its position in the array
            var inPlace = arr.Select((n, i) => new { Number = n, InPlace = n == i });

            Console.WriteLine("Number: In-place?");
            foreach (var item in inPlace)
                Console.WriteLine($"{item.Number}: {item.InPlace}");
            #endregion

            #region Q5 Pairs where numbersA < numbersB
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var pairs = numbersA.SelectMany(a => numbersB.Where(b => a < b), (a, b) => new { a, b });

            Console.WriteLine("Pairs where a < b:");
            foreach (var pair in pairs)
                Console.WriteLine($"{pair.a} is less than {pair.b}");
            #endregion

            #region Q6 Orders where the total is less than 500
            var smallOrders = CustomersList.SelectMany(c => c.Orders).Where(o => o.Total < 500.00M);

            foreach (var item in smallOrders)
                Console.WriteLine(item);
            #endregion

            #region Q7 Orders made in 1998 or later
            var newOrders = CustomersList.SelectMany(c => c.Orders).Where(o => o.OrderDate.Year >= 1998);

            foreach (var item in newOrders)
                Console.WriteLine(item);
            #endregion

            #endregion
        }
    }
}
