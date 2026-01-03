using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using ConsoleTables;
using MarketSystem.Data.Enums;
using MarketSystem.Data.Models;
using MarketSystem.Services.Abstract;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarketSystem.Services.Concrete
{
	public class MenuService
	{
        private static IMarketable marketService = new MarketService();

        public static void MenuGetProducts()
        {
            var products = marketService.GetProducts();

            if(products.Count() == 0)
            {
                Console.WriteLine("Products list is empty!");
                return;
            }

            var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

            foreach (var product in products!)
            {
                table.AddRow(product.ID, product.Name, product.Category, product.Count, product.Price);
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

                Console.WriteLine($"Product with ID:{id} was updated!");

            }
            catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

        public static void MenuGetProductsByCategory()
        {
            try
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
                    Console.WriteLine("Invalid category.");
                    return;
                }

                var products = marketService.GetProductsByCategory(category);

                var table2 = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in products!)
                {
                    table2.AddRow(product.ID, product.Name, product.Category, product.Count, product.Price);
                }

                table2.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void MenuGetProductsByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter min Price: ");
                int minPrice = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter max Price: ");
                int maxPrice = int.Parse(Console.ReadLine()!);

                var products = marketService.GetProductsByPriceRange(minPrice, maxPrice);

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in products!)
                {
                    table.AddRow(product.ID, product.Name, product.Category, product.Count, product.Price);
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

                var table = new ConsoleTable("ID", "Name", "Category", "Count", "Price");

                foreach (var product in products!)
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


        public static void MenuGetSales()
        {
            var sales = marketService.GetSales();

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
                Console.WriteLine("Enter product's ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var sale = marketService.GetSale(id);

                var table1 = new ConsoleTable("Sale ID", "Amount", "Products Count", "Date");

                table1.AddRow(sale.ID, sale.Amount, sale.SaleItems.Count(), sale.Date);
                
                table1.Write();
                Console.WriteLine("====================================================");

                var table2 = new ConsoleTable("Product ID", "Name", "Count");

                foreach (var saleItem in sale.SaleItems)
                {
                    table2.AddRow(saleItem.ID, saleItem.Product.Name, saleItem.Quantity);
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
                int minAmount = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter max amount: ");
                int maxAmount = int.Parse(Console.ReadLine()!);

                var sales = marketService.GetSalesByAmountRange(minAmount,maxAmount);

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
                Console.WriteLine("Enter date (dd.MM.yyyy HH:mm:ss):");
                var date = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

                var table = new ConsoleTable("Sale's ID", "Amount", "Date");

                foreach (var sale in marketService.GetSalesByDate(date))
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

        public static void MenuGetSalesByDateRange()
        {
            try
            {
                Console.WriteLine("Enter min date (dd.MM.yyyy HH:mm:ss):");
                var minDate = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

                Console.WriteLine("Enter max date (dd.MM.yyyy HH:mm:ss):");
                var maxDate = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm:ss", null);

                var table = new ConsoleTable("Sale's ID", "Amount", "Products Count", "Date");

                foreach (var sale in marketService.GetSalesByDateRange(minDate, maxDate))
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

        public static void MenuAddSale()
        {
            try
            {
                string option;
                Dictionary<int, int> products = new Dictionary<int, int>();
                do
                {
                    MenuGetProducts();

                    Console.WriteLine("Enter product ID: ");
                    int productID = int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Enter product count: ");
                    int count = int.Parse(Console.ReadLine()!);

                    if (products.ContainsKey(productID))
                        products[productID] = (int)products[productID]! + count;
                    else
                        products.Add(productID, count);

                    Console.WriteLine("Do you want to continue: ");
                    option = Console.ReadLine()!;

                } while (!option.Equals("no", StringComparison.OrdinalIgnoreCase));

                marketService.AddSale(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

