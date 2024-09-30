using System;
using System.Text;
using Newtonsoft.Json;
using sassy.bulk.Cache;
using sassy.bulk.Webhooks;
using sassy.bulk.Endpoints;
using System.Threading.Tasks;
using sassy.bulk.ResponseDto;
using sassy.bulk.UIUtil.Abstract;
using System.Diagnostics;

namespace sassy.bulk.UIUtil
{
    public class ListSaleInvoiceScreen : Base
    {
        public override void StartScreen()
        {
            var data = new Connect360Response();
            int pageNo = 1;
            int pageSize = 10;
            Console.WriteLine("-------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Remarks: Data format like (09/05/24)");
            Console.ForegroundColor = ConsoleColor.White;
            label:
            string startDate = TakeInput("Enter Start Date: ");
            string endDate = TakeInput("Enter End Date: ");

            Console.WriteLine("Loading Data.............");
            Console.WriteLine("\r");
            var tracker = Stopwatch.StartNew();
            data = FetchSalesInvoice(startDate: startDate, endDate: endDate).GetAwaiter().GetResult();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\r");
                if (data.Data is null)
                {
                    Console.WriteLine("\r");
                    Console.WriteLine("No more data found");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                if(data.Data != null)
                {
                    tracker.Stop();
                    DisplayPrettyData(data);
                    DisplayCompletionTime(tracker.Elapsed);
                }
                Console.ForegroundColor = ConsoleColor.White;
                string wantNext = Input("Display next?: ");

                if (wantNext.ToLower() != "yes" && wantNext.ToLower() != "y")
                {
                    break;
                }
                tracker.Restart();
                pageNo++;
                data = FetchSalesInvoice(pageNo,pageSize).GetAwaiter().GetResult();
            }

            string backMenu = Input("Back to menu: ");
            if(backMenu == "no" || backMenu == "n")
            {
                goto label;
            }
            var saleInvoice = new SaleInvoiceScreen();
            saleInvoice.StartScreen();
        }
        private async Task<Connect360Response> FetchSalesInvoice(int pageNo = 0, int pageSize = 10, string filterValue = "", string startDate = "09/19/2024", string endDate = "09/19/2024")
        {
            var token = GetData(CacheKey.BearerToken);
            
            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.OrderService);
            builder.Append(ClientEndPoints.Api);
            builder.Append(ClientEndPoints.InvoiceFilter);

            var formattedEndpoint = builder.ToString();
            
            Webhook.BearerToken = token.ToString();

            var requestData = await Webhook.SendAsync(formattedEndpoint, PrepaireRequestBody(pageNo,pageSize,filterValue,startDate, endDate), "application/json", AddHeaders()).ConfigureAwait(false);
            
            var stringContent = await requestData.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseObj = JsonConvert.DeserializeObject<Connect360Response>(stringContent);
            return responseObj;
        } 
    }
}
