using System;
using ConsoleTables;
using MarketSystem.Data.Enums;
using MarketSystem.Data.Models;
using MarketSystem.Services.Abstract;

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
                Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine()!);

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
                Category category = (Category)Enum.Parse(typeof(Category), Console.ReadLine()!);

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

    }
}

