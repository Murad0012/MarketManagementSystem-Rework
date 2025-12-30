using System;
using MarketSystem.Services.Concrete;

namespace MarketSystem.Helpers
{
	public class SubMenuHelper
	{
		public static void DisplayProductMenu()
		{
            int selectOption;

            do
            {
                Console.WriteLine("1.Add Product");
                Console.WriteLine("2.Update Product");
                Console.WriteLine("3.Delete Product");
                Console.WriteLine("4.Show Products");
                Console.WriteLine("5.Get Product by category");
                Console.WriteLine("6.Get Product by Price range");
                Console.WriteLine("7.Get Product by name");

                Console.WriteLine("0.Exit");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Please, select an option:");

                while (!int.TryParse(Console.ReadLine(), out selectOption))
                {
                    Console.WriteLine("Please enter valid option");
                }

                switch (selectOption)
                {
                    case 1:
                        MenuService.MenuAddProduct();
                        break;
                    case 2:
                        
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectOption != 0);
        }

        public static void DisplaySaleMenu()
        {
            int selectOption;

            do
            {
                Console.WriteLine("1.Add Sale");
                Console.WriteLine("2.Return Sale Product");
                Console.WriteLine("3.Delete Sale");
                Console.WriteLine("4.Get Sales");
                Console.WriteLine("5.Get Sales by Date range");
                Console.WriteLine("6.Get Sales by Amount range");
                Console.WriteLine("7.Get Sales by date");
                Console.WriteLine("8.Get Sales by ID");

                Console.WriteLine("0.Exit");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Please, select an option:");

                while (!int.TryParse(Console.ReadLine(), out selectOption))
                {
                    Console.WriteLine("Please enter valid option");
                }

                switch (selectOption)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectOption != 0);
        }

    }
}

