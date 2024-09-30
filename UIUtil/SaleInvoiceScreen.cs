using System;
using sassy.bulk.BotHelper;
using sassy.bulk.UIUtil.Abstract;

namespace sassy.bulk.UIUtil
{
    public class SaleInvoiceScreen : Base
    {
        public override void StartScreen()
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Bot.Welcome}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Select your option below");
            Console.WriteLine("1. Add Sale Invoice");
            Console.WriteLine("2. View Sale Invoice Data");
            Console.WriteLine("3. Back");
            Console.WriteLine();

            int choice = Input("Enter your choice: ", 1, 3);
            switch (choice)
            {
                case 1:
                    var addInvoice = new AddSaleInvoiceScreen();
                    addInvoice.StartScreen();
                    break;
                case 2:
                    var listDetails = new ListSaleInvoiceScreen();
                    listDetails.StartScreen();
                    break;
                case 3:
                    var mainScreen = new MainScreen();
                    mainScreen.StartScreen();
                    break;
                case 4:
                    Back();
                    break;
            }
        }
    }
}
