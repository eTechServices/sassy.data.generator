using System;
using System.Threading.Tasks;
using sassy.bulk.UIUtil.Abstract;

namespace sassy.bulk.UIUtil
{
    public class RestartApplication : Base
    {
        public override void StartScreen()
        {
            Console.WriteLine("\nDo you want to open the menu?");
            string choice = Input("Yes or No: ");
            ContinueExecution(choice).GetAwaiter().GetResult();
        }
        private async Task ContinueExecution(string choice)
        {
            switch (choice)
            {
                case "yes":
                case "y":
                    var main = new MainScreen();
                    main.StartScreen();
                    break;
                case "no":
                case "n":
                    Console.WriteLine("Are you sure to close this application?");
                    string input = Input("Yes or  No: ");
                    switch (input)
                    {
                        case "no":
                        case "n":
                            var againMain = new MainScreen();
                            againMain.StartScreen();
                            break;
                        case "yes":
                        case "y":
                            KillApplication();
                            break;
                    }
                    break;
            }
        }
    }
}
