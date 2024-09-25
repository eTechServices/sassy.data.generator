using InvoiceBulkRegisteration.Dtos;
using sassy.bulk.UIUtil.Abstract;
using System;

namespace sassy.bulk.UIUtil
{
    public class TakeCustomerDetail : Base
    {
        public override void StartScreen()
        {
            Console.WriteLine("Please enter customer details");
            Console.WriteLine("-----------------------------");
            _ = TakeDetails();

            Console.WriteLine("Do you want to Add another?");
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
