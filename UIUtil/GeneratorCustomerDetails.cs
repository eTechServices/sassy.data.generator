using sassy.bulk.DataGenerator;
using sassy.bulk.UIUtil.Abstract;
using System;
using System.Linq;

namespace sassy.bulk.UIUtil
{
    public class GeneratorCustomerDetails : Base
    {
        public override void StartScreen()
        {
            Console.WriteLine("How many customer do you want to add?");
            Console.WriteLine("Remarks limited: Max 1000");
            int choice = Input("Enter number to generate fake customers: ", 1, 1000);
            if(choice > 1)
            {
                var fakecustomer = Trainner.GetCustomerData(choice);
                if (fakecustomer.Count() > 0)
                {
                    Console.WriteLine("Do you want to save it to Sassy POS");
                    string input = Input("Yes or No: ");
                    switch (input)
                    {
                        case "yes":
                        case "y":
                            foreach (var item in fakecustomer)
                            {
                                int currentIteration = fakecustomer.ToList().IndexOf(item) + 1;
                                ProgressBar(currentIteration, choice);
                            }
                            break;
                        case "no":
                        case "n":

                            break;
                    }
                }
            }
        }
    }
}
