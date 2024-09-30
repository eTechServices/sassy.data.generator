using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using sassy.bulk.Cache;
using System.Diagnostics;
using sassy.bulk.Webhooks;
using sassy.bulk.Endpoints;
using System.Threading.Tasks;
using sassy.bulk.DataGenerator;
using sassy.bulk.UIUtil.Abstract;
using InvoiceBulkRegisteration.Dtos;

namespace sassy.bulk.UIUtil
{
    public class GeneratorCustomerDetails : Base
    {
        public override void StartScreen()
        {
            Console.WriteLine("Remarks limited: Max 1000");
            int choice = Input("Enter number to generate fake customers: ", 1, 1000);
            if(choice > 1)
            {
                var fakecustomer = Trainner.GetCustomerData(choice);
                if (fakecustomer.Count() > 0)
                {
                    Console.WriteLine("Do you want to save it to Sassy POS");
                    string input = Input("Yes or No: ");
                    var tracker = Stopwatch.StartNew();
                    switch (input)
                    {
                        case "yes":
                        case "y":
                            foreach (var item in fakecustomer)
                            {
                                int currentIteration = fakecustomer.ToList().IndexOf(item) + 1;
                                _ = PresistCustomer(item).GetAwaiter().GetResult();
                                ProgressBar(currentIteration, choice);
                            }
                            tracker.Stop();
                            DisplayCompletionTime(tracker.Elapsed);
                            break;
                        case "no":
                        case "n":
                            foreach (var item in fakecustomer)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                int currentIteration = fakecustomer.ToList().IndexOf(item) + 1;
                                PrintData(JsonConvert.SerializeObject(item,Formatting.Indented));
                            }
                            tracker.Stop();
                            DisplayCompletionTime(tracker.Elapsed);
                            break;
                    }
                }
            }
        }
        private async Task<bool> PresistCustomer(SampleCustomerDto customerDto)
        {
            if (customerDto != null)
            {
                var token = GetData(CacheKey.BearerToken);
                var builder = new StringBuilder();
                builder.Append(ClientEndPoints.BaseSassylUrl);
                builder.Append(ClientEndPoints.Api);
                builder.Append(ClientEndPoints.CreateCustomer);
                var formattedEndpoint = builder.ToString();
                Webhook.BearerToken = token.ToString();
                _ = await Webhook.SendAsync(formattedEndpoint, customerDto, "application/json", AddHeaders());
                return true;
            }
            return false;
        }
    }
}
