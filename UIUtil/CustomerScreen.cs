using sassy.bulk.BotHelper;
using sassy.bulk.UIUtil.Abstract;
using System;

namespace sassy.bulk.UIUtil
{
    public class CustomerScreen : Base
    {
        public override void StartScreen()
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Bot.Welcome}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Select your option below");
            Console.WriteLine("1. Add Customer Auto");
            Console.WriteLine("2. Add Customer Manually");
            Console.WriteLine("3. Search Customer");
            Console.WriteLine("4. Back");
            Console.WriteLine();

            int choice = Input("Enter your choice: ", 1, 4);
            switch (choice)
            {
                case 1:
                    var fakeCustomer = new GeneratorCustomerDetails();
                    fakeCustomer.StartScreen();
                    break;
                case 2:
                    var takeCustomerDetails = new TakeCustomerDetail();
                    takeCustomerDetails.StartScreen();
                    break;
                case 3:
                    var verify = new DBCustomer();
                    verify.StartScreen();
                    break;
                case 4:
                    Back();
                    break;
            }

        }
    }
}
