using Bogus;
using InvoiceBulkRegisteration.Dtos;
using System;

namespace ModelTraining.Trainner
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class CustomerDtoTrainner : Faker<SampleCustomerDto>
    {
        public CustomerDtoTrainner()
        {
            RuleFor(c => c.Id, f => DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString())
           .RuleFor(c => c.FirstName, f => f.Name.FirstName())
           .RuleFor(c => c.LastName, f => f.Name.LastName())
           .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
           .RuleFor(c => c.Email, f => f.Internet.Email())
           .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
           .RuleFor(c => c.State, f => f.Address.State())
           .RuleFor(c => c.City, f => f.Address.City());
        }
    }
}
