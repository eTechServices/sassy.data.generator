using InvoiceBulkRegisteration.Dtos;
using Newtonsoft.Json;
using sassy.bulk.Cache;
using sassy.bulk.DataGenerator;
using sassy.bulk.Endpoints;
using sassy.bulk.UIUtil.Abstract;
using sassy.bulk.Webhooks;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                _ = PresistCustomer(item).GetAwaiter().GetResult();
                                ProgressBar(currentIteration, choice);
                            }
                            break;
                        case "no":
                        case "n":
                            foreach (var item in fakecustomer)
                            {
                                int currentIteration = fakecustomer.ToList().IndexOf(item) + 1;
                                ProgressBar(currentIteration, choice);
                                PrintData(JsonConvert.SerializeObject(item));
                            }
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
                builder.Append(ClientEndPoints.Localhost);
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
