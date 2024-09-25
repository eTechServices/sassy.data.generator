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
            Console.WriteLine($"{Bot.Welcome}");
            Console.WriteLine("Select your option below");
            Console.WriteLine("1. Add Customer Auto");
            Console.WriteLine("2. Add Customer Manually");
            Console.WriteLine("3. Verify Customer from App");
            Console.WriteLine();

            int choice = Input("Enter your choice: ", 1, 3);
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
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        }
    }
}
