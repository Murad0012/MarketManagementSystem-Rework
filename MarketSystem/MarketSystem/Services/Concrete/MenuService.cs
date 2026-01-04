using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using ConsoleTables;
using MarketSystem.Data.Enums;
using MarketSystem.Data.Models;
using MarketSystem.Services.Abstract;
using Microsoft.VisualBasic.FileIO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarketSystem.Services.Concrete
{
	public class MenuService
	{
        private static IMarketable marketService = new MarketService();

        // Product methods
        public static void MenuGetProducts()
        {
            var products = marketService.GetProducts();

            if(products.Count == 0)
            {
                Console.WriteLine("Product list is empty!");
                return;
            }

            var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

            foreach (var product in products!)
            {
                table.AddRow(
                    product.ID,
                    product.Name,
                    product.Category,
                    product.Count,
                    product.Price
                    );
            }

            table.Write();
        }

        public static void MenuAddProduct()
		{
			try
			{
                Console.WriteLine("Enter product's name:");
                string name = Console.ReadLine()!;

				var table = new ConsoleTable("Category");

                var categories = Enum.GetNames(typeof(Category));

				int option = 1;
				foreach (string item in categories)
				{
					table.AddRow($"{item} - {option++}");
				}

				table.Write();

                Console.WriteLine("Enter product's category:");
                Category category = Enum.Parse<Category>(Console.ReadLine()!);

                Console.WriteLine("Enter product's price: ");
				decimal price = decimal.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter product's count: ");
                int count = int.Parse(Console.ReadLine()!);

				int id = marketService.AddProduct(name, price, category, count);

                Console.WriteLine($"Product with ID:{id} was created!");

            }
            catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

        public static void MenuDeleteProduct()
        {
            try
            {
                Console.WriteLine("Enter product's ID: ");
                int id = int.Parse(Console.ReadLine()!);

                marketService.DeleteProduct(id);

                Console.WriteLine($"Product with ID:{id} was deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MenuUpdateProduct()
		{
			try
			{
				MenuGetProducts();

                Console.WriteLine("Enter product's ID: ");
                int id = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter product's name:");
                string name = Console.ReadLine()!;

                Console.WriteLine("Enter product's category: ");
                var table = new ConsoleTable("Category");

                var categories = Enum.GetNames(typeof(Category));

                int option = 1;
                foreach (string item in categories)
                {
                    table.AddRow($"{item} - {option++}");
                }

                table.Write();

                Console.WriteLine("Enter product's category:");
                Category category = Enum.Parse<Category>(Console.ReadLine()!);

                Console.WriteLine("Enter product's price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter product's count: ");
                int count = int.Parse(Console.ReadLine()!);

                marketService.UpdateProduct(id, name, price, category, count);

                Console.WriteLine($"Product with ID:{id} was updated.");

            }
            catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

        public static void MenuGetProductsByCategory()
        {
            var table1 = new ConsoleTable("Category");

            var categories = Enum.GetNames(typeof(Category));

            int option = 1;
            foreach (string item in categories)
            {
                table1.AddRow($"{item} - {option++}");
            }

            table1.Write();

            Console.WriteLine("Enter product's category:");
            if (!Enum.TryParse<Category>(Console.ReadLine(), out var category))
            {
                Console.WriteLine("No products found in this category.");
                return;
            }

            var products = marketService.GetProductsByCategory(category);

            var table2 = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

            foreach (var product in products)
            {
                table2.AddRow(product.ID, product.Name, product.Category, product.Count, product.Price);
            }

            table2.Write();
        }

        public static void MenuGetProductsByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter min Price: ");
                decimal minPrice = decimal.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter max Price: ");
                decimal maxPrice = decimal.Parse(Console.ReadLine()!);

                var products = marketService.GetProductsByPriceRange(minPrice, maxPrice);

                if (products.Count == 0)
                {
                    Console.WriteLine("No products found in this price range.");
                    return;
                }

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in products)
                {
                    table.AddRow
                        (product.ID,
                        product.Name,
                        product.Category,
                        product.Count,
                        product.Price
                        );
                }

                table.Write();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MenuGetProductsByName()
        {
            try
            {
                Console.WriteLine("Enter product's name:");
                string name = Console.ReadLine()!;

                var products = marketService.GetProductsByName(name);

                if (products.Count == 0)
                {
                    Console.WriteLine("No products found with this name.");
                    return;
                }

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in products)
                {
                    table.AddRow(product.ID, product.Name, product.Category, product.Count, product.Price);
                }

                table.Write();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Sale methods
        public static void MenuGetSales()
        {
            var sales = marketService.GetSales();

            if (sales.Count == 0)
            {
                Console.WriteLine("No sales found.");
                return;
            }

            var table = new ConsoleTable("ID", "Amount", "Products Count", "Date");

            foreach (var sale in sales)
            {
                table.AddRow(sale.ID, sale.Amount, sale.SaleItems.Count, sale.Date);
            }

            table.Write();
        }

        public static void MenuGetSale()
        {
            try
            {
                Console.WriteLine("Enter sale's ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var sale = marketService.GetSale(id);

                var table1 = new ConsoleTable("Sale ID", "Amount", "Products Count", "Date");

                table1.AddRow(sale.ID, sale.Amount, sale.SaleItems.Count, sale.Date);
                
                table1.Write();
                Console.WriteLine("====================================================");

                var table2 = new ConsoleTable("Product ID", "Name", "Count");

                foreach (var saleItem in sale.SaleItems)
                {
                    table2.AddRow(
                        saleItem.Product.ID,
                        saleItem.Product.Name,
                        saleItem.Quantity
                    );
                }

                table2.Write();
                Console.WriteLine("====================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MenuGetSalesByAmountRange()
        {
            try
            {
                Console.WriteLine("Enter min amount: ");
                decimal minAmount = decimal.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter max amount: ");
                decimal maxAmount = decimal.Parse(Console.ReadLine()!);

                var sales = marketService.GetSalesByAmountRange(minAmount,maxAmount);

                if (sales.Count == 0)
                {
                    Console.WriteLine("No sales found in this amount range.");
                    return;
                }

                var table = new ConsoleTable("ID", "Amount", "Products Count", "Date");

                foreach (var sale in sales)
                {
                    table.AddRow(sale.ID, sale.Amount, sale.SaleItems.Count, sale.Date);
                }

                table.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MenuGetSalesByDate()
        {
            try
            {
                Console.WriteLine("Enter date (dd.MM.yyyy):");
                var date = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy", null);

                var table = new ConsoleTable("Sale's ID", "Amount", "Date");

                var sales = marketService.GetSalesByDate(date);

                if (sales.Count == 0)
                {
                    Console.WriteLine("No sales found on this date.");
                    return;
                }

                foreach (var sale in sales)
                {
                    table.AddRow(sale.ID, sale.Amount, sale.Date);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void MenuGetSalesByDateRange()
        {
            try
            {
                Console.WriteLine("Enter minumum (dd.MM.yyyy HH:mm:ss):");
                var minDate = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

                Console.WriteLine("Enter maximum date (dd.MM.yyyy HH:mm:ss):");
                var maxDate = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

                var table = new ConsoleTable("Sale's ID", "Amount", "Date");

                foreach (var sale in marketService.GetSalesByDateRange(minDate, maxDate))
                {
                    table.AddRow(sale.ID, sale.Amount, sale.Date);
                }

                table.Write();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void MenuAddSale()
        {
            try
            {
                string option;
                Dictionary<int, int> products = new Dictionary<int, int>();
                MenuGetProducts();
                do
                {
                    Console.WriteLine("Enter product ID: ");
                    int productID = int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Enter product count: ");
                    int count = int.Parse(Console.ReadLine()!);

                    if (products.ContainsKey(productID))
                        products[productID] = products[productID]! + count;
                    else
                        products.Add(productID, count);

                    Console.WriteLine("Do you want to continue: ");
                    option = Console.ReadLine()!;

                } while (!option.Equals("no", StringComparison.OrdinalIgnoreCase));

                marketService.AddSale(products);

                Console.WriteLine("Finished!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MenuDeleteSale()
        {
            try
            {
                MenuGetSales();

                Console.WriteLine("Enter sale's ID: ");
                int id = int.Parse(Console.ReadLine()!);

                marketService.DeleteSale(id);

                Console.WriteLine($"Sale with ID:{id} was deleted!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MenuReturnProductFromSale()
        {
            try
            {
                MenuGetSales();

                Console.WriteLine("Complete ");

                Console.WriteLine("Enter sale's ID: ");
                int saleID = int.Parse(Console.ReadLine()!);

                string option;
                Dictionary<int, int> products = new Dictionary<int, int>();
                do
                {
                    Console.WriteLine("Enter product's ID: ");
                    int productID = int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Enter product's count: ");
                    int count = int.Parse(Console.ReadLine()!);

                    if (products.ContainsKey(productID))
                        products[productID] = products[productID]! + count;
                    else
                        products.Add(productID, count);

                    Console.WriteLine("Do you want to continue: ");
                    option = Console.ReadLine()!;

                } while (!option.Equals("no", StringComparison.OrdinalIgnoreCase));

                marketService.ReturnProductFromSale(saleID, products);

                Console.WriteLine("Complete");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

