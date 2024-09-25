using Newtonsoft.Json;
using sassy.bulk.Endpoints;
using sassy.bulk.ResponseDto;
using sassy.bulk.UIUtil.Abstract;
using sassy.bulk.Webhooks;
using System;
using System.Text;

namespace sassy.bulk.UIUtil
{
    public class DBCustomer : Base
    {
        public override async void StartScreen()
        {
            var customerId = TakeInput("Enter customer Id: ");
            string token = TakeInput("Enter JWT token: ");


            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.CustomerService);
            builder.Append(ClientEndPoints.SearchCustomer);
            builder.Append(customerId);

            Webhook.BearerToken = token;
            var data = await Webhook.SendAsync<Connect360Response, Connect360Response>(builder.ToString(), "application/json").ConfigureAwait(false);
            if (data is Connect360Response)
            {
                Print(JsonConvert.SerializeObject(data));
            }

        }
    }
}
