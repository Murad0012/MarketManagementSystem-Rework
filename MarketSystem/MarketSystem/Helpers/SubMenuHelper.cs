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
                Console.WriteLine("1.Add product");
                Console.WriteLine("2.Delete product");
                Console.WriteLine("3.Update product");
                Console.WriteLine("4.Show products");
                Console.WriteLine("5.Get product by category");
                Console.WriteLine("6.Get product by price range");
                Console.WriteLine("7.Get product by name");

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
                        MenuService.MenuDeleteProduct();
                        break;
                    case 3:
                        MenuService.MenuUpdateProduct();
                        break;
                    case 4:
                        MenuService.MenuGetProducts();
                        break;
                    case 5:
                        MenuService.MenuGetProductsByCategory();
                        break;
                    case 6:
                        MenuService.MenuGetProductsByPriceRange();
                        break;
                    case 7:
                        MenuService.MenuGetProductsByName();
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
                Console.WriteLine("1.Add sale");
                Console.WriteLine("2.Delete sale");
                Console.WriteLine("3.Return sale product");
                Console.WriteLine("4.Get sales");
                Console.WriteLine("5.Get sales by ID");
                Console.WriteLine("6.Get sales by date");
                Console.WriteLine("7.Get sales by date range");
                Console.WriteLine("8.Get sales by amount range");

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
                        MenuService.MenuAddSale();
                        break;
                    case 2:
                        MenuService.MenuDeleteSale();
                        break;
                    case 3:
                        MenuService.MenuReturnProductFromSale();
                        break;
                    case 4:
                        MenuService.MenuGetSales();
                        break;
                    case 5:
                        MenuService.MenuGetSale();
                        break;
                    case 6:
                        MenuService.MenuGetSalesByDate();
                        break;
                    case 7:
                        MenuService.MenuGetSalesByDateRange();
                        break;
                    case 8:
                        MenuService.MenuGetSalesByAmountRange();
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

