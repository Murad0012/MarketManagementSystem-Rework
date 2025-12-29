using MarketSystem.Helpers;

namespace MarketSystem;

class Program
{
    static void Main(string[] args)
    {
        int selectOption;

        Console.WriteLine("=====Welcome=====");
        do
        {
            Console.WriteLine("1.For managing Product");
            Console.WriteLine("2.For managing Sale");

            Console.WriteLine("0. Exit");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Please, select an option:");

            while (!int.TryParse(Console.ReadLine(), out selectOption))
            {
                Console.WriteLine("Please enter valid option");
            }

            switch (selectOption)
            {
                case 1:
                    SubMenuHelper.DisplayProductMenu();
                    break;
                case 2:
                    SubMenuHelper.DisplaySaleMenu();
                    break;
                case 0:
                    Console.WriteLine("Bye!");
                    break;
                default:
                    Console.WriteLine("No such option!");
                    break;
            }
        } while (selectOption != 0);
    }
}

