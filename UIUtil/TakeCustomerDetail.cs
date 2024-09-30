using InvoiceBulkRegisteration.Dtos;
using Newtonsoft.Json;
using sassy.bulk.Cache;
using sassy.bulk.Endpoints;
using sassy.bulk.UIUtil.Abstract;
using sassy.bulk.Webhooks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace sassy.bulk.UIUtil
{
    public class TakeCustomerDetail : Base
    {
        public override void StartScreen()
        {
            var dataset = new List<SampleCustomerDto>();
            Console.WriteLine("Please enter customer details");
            Console.WriteLine("-----------------------------");
            while (true)
            {
                var customerData = TakeDetails();
                dataset.Add(customerData);
                string isAnother = Input("Do you want to Add another?: ");
                if (isAnother == "yes" || isAnother == "y")
                {
                    int howMany = InputInt("How many do you want to add: ");
                    
                    for(int i = 0; i < howMany; i++)
                    {
                        Console.WriteLine($"--------------{dataset.Count}--------------");
                        dataset.Add(TakeDetails());
                    }

                    string presist = Input("Save data?: ");
                    if (presist == "yes" || presist == "y")
                    {
                        PresistCustomer(dataset);
                    }
                    Console.WriteLine($"{dataset.Count} customers added successfully");
                }
                break;
            }
        }
        private void PresistCustomer(List<SampleCustomerDto> dataSet)
        {
            var token = GetData(CacheKey.BearerToken);
            var builder = new StringBuilder();
            builder.Append(ClientEndPoints.BaseSassylUrl);
            builder.Append(ClientEndPoints.Api);
            builder.Append(ClientEndPoints.CreateCustomer);
            var formattedEndpoint = builder.ToString();
            Webhook.BearerToken = token.ToString();
            
            Parallel.ForEach(dataSet, customer =>
            {
                _ = Webhook.SendAsync(formattedEndpoint, customer, "application/json", AddHeaders()).ConfigureAwait(false);

            });
        }
        private SampleCustomerDto TakeDetails()
        {
            string firstName = TakeInput("First Name: ");
            string lastName = TakeInput("Last Name: ");
            string phone = TakeInput("Phone Number: ");
            string email = TakeInput("Email: ");
            string postalCode = TakeInput("Postal Code: ");
            string city = TakeInput("City: ");

            SampleCustomerDto customer = new SampleCustomerDto
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                PostalCode = postalCode,
                City = city,
            };
            return customer;
        } 
    }
}
