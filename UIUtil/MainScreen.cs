using sassy.bulk.BotHelper;
using sassy.bulk.UIUtil.Abstract;
using System;

namespace sassy.bulk.UIUtil
{
    public class MainScreen: Base
    {
        public override void StartScreen()
        {
            Clear();
            Console.WriteLine($"{Bot.Welcome}");
            Console.WriteLine("Welcome to SassyPOS Bulk Data Entry");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Sales Invoices");
            Console.WriteLine("3. Exit");
            Console.WriteLine();

            int choice = Input("Enter your choice: ", 1, 3);

            switch (choice)
            {
                case 1:
                    var customer = new CustomerScreen();
                    customer.StartScreen();
                    break;
                case 2:
                    var sale = new SaleInvoiceScreen();
                    sale.StartScreen();
                    break;
                case 3:
                    KillApplication();
                    break;
            }
        }
        
    }
}
