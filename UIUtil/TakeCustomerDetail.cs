using InvoiceBulkRegisteration.Dtos;
using Newtonsoft.Json;
using sassy.bulk.UIUtil.Abstract;
using System;
using System.Collections.Generic;

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
                    Console.WriteLine($"{dataset.Count} customers added successfully");
                    Console.WriteLine(JsonConvert.SerializeObject(dataset));
                }
                break;
            }
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
