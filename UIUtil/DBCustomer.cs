using System;
using System.Text;
using Newtonsoft.Json;
using sassy.bulk.Cache;
using System.Diagnostics;
using sassy.bulk.Webhooks;
using sassy.bulk.Endpoints;
using sassy.bulk.ResponseDto;
using sassy.bulk.UIUtil.Abstract;

namespace sassy.bulk.UIUtil
{
    public class DBCustomer : Base
    {
        public override void StartScreen()
        {
            label:
            string customerName = TakeInput("Enter customer name: ");


            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.CustomerService);
            builder.Append(ClientEndPoints.Api);
            builder.Append(ClientEndPoints.GetCustomers);
            builder.Replace("{{pageSize}}", $"10");
            builder.Replace("{{CName}}", customerName);
            var stopwatch = Stopwatch.StartNew();
            Webhook.BearerToken = GetData(CacheKey.BearerToken).ToString();
            var data = Webhook.SendAsync<Connect360Response, Connect360Response>(builder.ToString(), "application/json", AddHeaders()).GetAwaiter().GetResult();
            if (data is Connect360Response)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                var responseObj = JsonConvert.DeserializeObject<Connect360Response>(JsonConvert.SerializeObject(data));
                DisplayPrettyData(responseObj);
            }
            stopwatch.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r");
            DisplayCompletionTime(stopwatch.Elapsed);
            Console.ForegroundColor = ConsoleColor.White;

            if (AskForOtther())
            {
                goto label;
            }
        }

        private bool AskForOtther()
        {
            string another = Input("Search another?: ");
            if(another == "yes" || another == "y")
            {
                return true;
            }
            return false;
        }
    }
}
